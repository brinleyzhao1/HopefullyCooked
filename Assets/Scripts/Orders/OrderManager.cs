using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public GameManager gameManager;
    public TMPro.TextMeshProUGUI requestText;
    public TMPro.TextMeshProUGUI timeLimitText;

    [SerializeField] List<OrderScript> possibleOrders;
    [SerializeField] float failScore;
    [SerializeField] bool inLevel1;
    [SerializeField] bool inLevel2;

    OrderScript currentOrder;
    float timeLimit = 120;
    int displayTime = 120;

    public bool freeze = true;
    // Start is called before the first frame update
    void Start()
    {
        GetNewOrder();
    }

    // Update is called once per frame
    void Update()
    {
        if (!freeze){
            timeLimit -= Time.deltaTime;
        }
       
        if (timeLimit <= 0) {
                displayTime = 0;
                FailOrder();
        }else{
                displayTime = (int)timeLimit;
        }
        timeLimitText.text = displayTime.ToString();
    }

    void SatisfyOrder(IngredientScript inScript){
        float modifier = inScript.scoreModifier;
        float totalOrderScore = currentOrder.satisfyingFoods[inScript.foodName]*modifier;
        gameManager.Score(totalOrderScore);
        GetNewOrder();
    }

    void FailOrder(){
        gameManager.Score(failScore);
        GetNewOrder();
    }

    void GetNewOrder(){
        currentOrder = possibleOrders[Random.Range(0, possibleOrders.Count)];
        timeLimit = currentOrder.timeLimit;
        displayTime = (int)timeLimit;
        requestText.text = currentOrder.request;
        timeLimitText.text = displayTime.ToString();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Ingredient>())
        {
            IngredientScript ingredientScript = other.gameObject.GetComponent<Ingredient>().ingredientScript;
            if (currentOrder.satisfyingFoods.ContainsKey(ingredientScript.foodName)){
                SatisfyOrder(ingredientScript);
            }else{
                FailOrder();
            }
            Destroy(other.gameObject);
        }
    }
}
