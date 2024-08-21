using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Market : MonoBehaviour
{
    public GameObject menu;
    public TextMeshProUGUI moneyAmount;
    public GameObject buyButton;
    public TextMeshProUGUI shipPrice;
    public TextMeshProUGUI message;
    private Player player;
    private int indexOfChoosenShip = 0;

    List<int> shipPrices = new List<int> { 0, 550, 850, 1000, 1500, 2000 };

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        indexOfChoosenShip = GameController.ChoosenShip - 1;
        message.text = "";
    }

    void Update()
    {
        if (GameController.UnlockedShips.Contains(indexOfChoosenShip + 1))
        {
            buyButton.SetActive(false);
        }
        else 
        {
            buyButton.SetActive(true);
        }

        moneyAmount.text = GameController.Money.ToString();
        ShipPrices();
    }

    // BUTTON ACTIONS
    public void PreviousShip()
    {
        if (indexOfChoosenShip != 0)
        {
            player.ships[indexOfChoosenShip].SetActive(false); // turn off current ship before activate previous one
            player.ships[indexOfChoosenShip - 1].SetActive(true); // turn on previous ship
            indexOfChoosenShip -= 1;
            SFXSoundController.buttonIsClicked = true;
        }
        else
        {
            SFXSoundController.isErrorPresented = true;
        }
    }

    public void NextShip()
    {
        if (indexOfChoosenShip != player.ships.Length - 1)
        {
            player.ships[indexOfChoosenShip].SetActive(false); // turn off current ship before activate next one
            player.ships[indexOfChoosenShip + 1].SetActive(true); // turn on next ship
            indexOfChoosenShip += 1;
            SFXSoundController.buttonIsClicked = true;
        }
        else
        {
            SFXSoundController.isErrorPresented = true;
        }
    }

    public void BuyAction()
    {
        if (shipPrices[indexOfChoosenShip] > GameController.Money)
        {
            SFXSoundController.isErrorPresented = true;
            message.text = "Not Enough Money";
            Invoke("HideMessage", 1.0f);
        }
        else 
        {
            SFXSoundController.isBought = true;
            GameController.Money -= shipPrices[indexOfChoosenShip];
            GameController.UnlockedShips.Add(indexOfChoosenShip + 1);
            GameController.SaveGameData();
        }
    }

    public void CloseButtonAction()
    {
        if (GameController.UnlockedShips.Contains(indexOfChoosenShip + 1))
        {
            SFXSoundController.buttonIsClicked = true;
            GameController.ChoosenShip = indexOfChoosenShip + 1;
            GameController.SaveGameData();
            menu.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else
        {
            SFXSoundController.isErrorPresented = true;
            message.text = "Choose the ship you own";
            Invoke("HideMessage", 1.0f);
        }
    }

    // METHODS
    private void HideMessage()
    {
        message.text = "";
    }

    private void ShipPrices()
    {
        switch (indexOfChoosenShip)
        {
            case 1:
            shipPrice.text = shipPrices[1].ToString();
            break;

            case 2:
            shipPrice.text = shipPrices[2].ToString();
            break;

            case 3:
            shipPrice.text = shipPrices[3].ToString();
            break;

            case 4:
            shipPrice.text = shipPrices[4].ToString();
            break;

            case 5:
            shipPrice.text = shipPrices[5].ToString();
            break;
        }
    }
}
