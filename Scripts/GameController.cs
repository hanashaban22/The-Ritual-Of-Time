using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Image[] slotIcons;
    public string[] slotItemNames;
    public EndMenuManager endMenuManager;

    private string selectedItem = "";
    private int activeSlotIndex = -1;

    void Start()
    {
        slotItemNames = new string[slotIcons.Length];
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Pickup"))
                {
                    Sprite itemSprite = hit.collider.GetComponent<SpriteRenderer>().sprite;
                    Color itemColor = hit.collider.GetComponent<SpriteRenderer>().color;

                    AddItemToInventory(itemSprite, itemColor, hit.collider.name);
                    Destroy(hit.collider.gameObject);
                }
                else if (hit.collider.name == "Door")
                {
                    if (selectedItem.Contains("Key"))
                    {
                        hit.collider.GetComponent<SpriteRenderer>().color = Color.green;
                        hit.collider.enabled = false;
                        RemoveSelectedItem();

                        if (endMenuManager != null)
                        {
                            endMenuManager.GoToMainMenu();
                        }
                    }
                }
            }
        }
    }

    void AddItemToInventory(Sprite itemSprite, Color itemColor, string itemName)
    {
        for (int i = 0; i < slotIcons.Length; i++)
        {
            if (!slotIcons[i].gameObject.activeSelf)
            {
                slotIcons[i].sprite = itemSprite;
                slotIcons[i].color = itemColor;
                slotItemNames[i] = itemName;
                slotIcons[i].gameObject.SetActive(true);
                break;
            }
        }
    }

    public void SelectSlot(int index)
    {
        if (slotIcons[index].gameObject.activeSelf)
        {
            selectedItem = slotItemNames[index];
            activeSlotIndex = index;
        }
    }

    void RemoveSelectedItem()
    {
        if (activeSlotIndex != -1)
        {
            slotIcons[activeSlotIndex].gameObject.SetActive(false);
            slotItemNames[activeSlotIndex] = "";
            selectedItem = "";
            activeSlotIndex = -1;
        }
    }
}