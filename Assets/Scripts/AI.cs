using System.Collections;
using UnityEngine;

public class AI : MonoBehaviour
{
    public MotherCell aIMotherCells;
    private MotherCell[] motherCells;
    private float analyzingTime = 3f;

    void Start()
    {
        motherCells = FindObjectsOfType<MotherCell>();
        aIMotherCells = GetComponent<MotherCell>();
        StartCoroutine(Analyze());
    }
   
    IEnumerator Analyze()
    {
        while (true)
        {
            yield return new WaitForSeconds(analyzingTime);
            aIMotherCells.SendUnit(RandomMotherCell());
        }
    }

    private GameObject RandomMotherCell()
    {
        return motherCells[Random.Range(0, motherCells.Length)].gameObject;
    }
}
