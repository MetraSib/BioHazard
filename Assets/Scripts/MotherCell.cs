using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MotherCell : MonoBehaviour
{
    private AudioSource unitDestroy;
    private ObjectPool objectPool;
    public Text numberOfCellsText;
    private SpriteRenderer spriteRenderer;
    public GameObject cellPrefab;
    public int numberOfCells;
    private int startNumberOfCells;
    private float xOffset;
    private float yOffset;
    private Vector3 offset;

    private enum Tags
    {
        Player, Enemy, Empty, UnitEnemy, UnitPlayer,Audio,UnitPool
    }

    private void Start()
    {
        unitDestroy = GameObject.FindGameObjectWithTag(Tags.Audio.ToString()).GetComponent<AudioSource>();
        objectPool = GameObject.FindGameObjectWithTag(Tags.UnitPool.ToString()).GetComponent<ObjectPool>();
        startNumberOfCells = numberOfCells;
        if (numberOfCells == 0) startNumberOfCells = 10;
        numberOfCellsText.text = numberOfCells.ToString();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Timer());
    }

    public void SendUnit(GameObject usableMotherCell)
    {
        if (usableMotherCell != gameObject)
        {
            for (int i = 0; i < numberOfCells / 2; i++)
            {
                xOffset = Random.Range(-0.4f, 0.4f);
                yOffset = Random.Range(-0.4f, 0.4f);
                offset = (transform.position + (Vector3.right * xOffset) + (Vector3.up * yOffset));
                var unitCell=objectPool.UnitInstantiate(offset, Quaternion.identity);
                if (CompareTag(Tags.Player.ToString())) unitCell.tag = Tags.UnitPlayer.ToString();
                if (CompareTag(Tags.Enemy.ToString())) unitCell.tag = Tags.UnitEnemy.ToString();
                var unitScript = unitCell.GetComponent<Unit>();
                unitScript.unitSprite.color= spriteRenderer.color;
                unitScript.SetTarget(usableMotherCell);
            }
            numberOfCells -= numberOfCells / 2;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var unitScript = collision.gameObject.GetComponent<Unit>();

        if (unitScript.usableMotherCell == gameObject)
        {
            unitDestroy.Play(0);

            if (CompareTag(Tags.Player.ToString()) && (collision.CompareTag(Tags.UnitPlayer.ToString())))
            {
                numberOfCells++;
                collision.gameObject.SetActive(false);
                
            }

            else if (CompareTag(Tags.Enemy.ToString()) && (collision.CompareTag(Tags.UnitEnemy.ToString())))
            {
                numberOfCells++;
                collision.gameObject.SetActive(false);
            }

            else
            {
                numberOfCells--;
                if (numberOfCells == 0)
                {
                    spriteRenderer.color = Color.gray;
                    tag = Tags.Empty.ToString();
                    var aI = GetComponent<AI>();
                    if (aI != null) Destroy(aI);
                }
                else if (numberOfCells < 0)
                {
                    numberOfCells = -numberOfCells;
                    if (collision.CompareTag(Tags.UnitEnemy.ToString()))
                    {
                        spriteRenderer.color = unitScript.unitSprite.color;
                        tag = Tags.Enemy.ToString();
                        gameObject.AddComponent<AI>();
                    }
                    else
                    {
                        spriteRenderer.color = unitScript.unitSprite.color;
                        tag = Tags.Player.ToString();
                        var aI = GetComponent<AI>();
                        if (aI != null) Destroy(aI);
                    }
                }
                collision.gameObject.SetActive(false);
            }
        }
        numberOfCellsText.text = numberOfCells.ToString();
    }
    
    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            if (numberOfCells < startNumberOfCells && !CompareTag(Tags.Empty.ToString()))
            {
                yield return StartCoroutine(PlusCells());
            }

            if (numberOfCells > startNumberOfCells && !CompareTag(Tags.Empty.ToString()))
            {
                yield return StartCoroutine(MinusCells());
            }
        }
    }

    IEnumerator PlusCells()
    {
        while (numberOfCells < startNumberOfCells)
        {
            yield return new WaitForSeconds(1f);
            numberOfCells++;
            numberOfCellsText.text = numberOfCells.ToString();
        }
    }

    IEnumerator MinusCells()
    {
        while (numberOfCells > startNumberOfCells)
        {
            yield return new WaitForSeconds(0.5f);
            numberOfCells--;
            numberOfCellsText.text = numberOfCells.ToString();
        }
    }
}
