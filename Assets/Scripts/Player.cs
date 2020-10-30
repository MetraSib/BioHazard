using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public UnityMonetization unityMonetization;
    public GameObject aura;
    public GameObject winMenu;
    public GameObject loseMenu;
    public List<GameObject> ChangedMotherCells;
    public GameObject usableMotherCell;
    private Camera mainCamera;
    public List<LineRenderer> lineRenderersList;
    private MotherCell[] motherCells;
    private enum Tags
    {
        Player, Enemy, Empty, UnitEnemy, UnitPlayer
    }
    private void Start()
    {
        motherCells = FindObjectsOfType<MotherCell>();
        mainCamera = Camera.main;
        StartCoroutine(WinOrLose());
    }

    void Update()
    {
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            int mask = 1 << 8;
            var touchPosition = mainCamera.ScreenToWorldPoint(Input.touches[0].position);
            Collider2D collider = Physics2D.Raycast(touchPosition, transform.position, Mathf.Infinity, mask).collider;
            if (CheckMotherCellList() && collider != null)
            {
                AddMotherCell(collider);
                aura.transform.position = collider.transform.position;
                aura.SetActive(true);
            }
             
        }
#endif
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            int mask = 1 << 8;
            var touchPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collider = Physics2D.Raycast(touchPosition, transform.position, Mathf.Infinity, mask).collider;
            if (CheckMotherCellList() && collider != null)
            {
                AddMotherCell(collider);
                aura.transform.position = collider.transform.position;
                aura.SetActive(true);
            }
        }
#endif
        else if (lineRenderersList.Count!=ChangedMotherCells.Count && lineRenderersList.Count >= 2)
        {
            aura.SetActive(false);
            ClearMotherCellsList();
        }

        else if (ChangedMotherCells.Count>=2)
        {
            aura.SetActive(false);
            for (int i = 0; i < ChangedMotherCells.Count; i++)
            {
                MotherCell motherCell = ChangedMotherCells[i].GetComponent<MotherCell>();
                motherCell.SendUnit(usableMotherCell);
            }
            ClearMotherCellsList();
        }
    }

    private void CheckWin()
    {
        bool loseResult = motherCells.All(u => !u.CompareTag(Tags.Player.ToString()));
        bool winResult = motherCells.All(u => !u.CompareTag(Tags.Enemy.ToString()));
        if (winResult)
        {
            unityMonetization.ShowADS();
            winMenu.SetActive(true);
            Time.timeScale = 0;
            int nextLevel = ++StaticSaveData.levelIndex;
            PlayerPrefs.SetString ("Level "+ nextLevel, "Level_"+ nextLevel);
            StaticSaveData.levelData = ("Level_" + nextLevel);
            StaticSaveData.levelIndex = nextLevel;
        }

        if (loseResult)
        {
            unityMonetization.ShowADS();
            loseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    IEnumerator WinOrLose()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            CheckWin();
        }
    }

    private void ClearMotherCellsList()
    {
        for (int i = 0; i < lineRenderersList.Count; i++)
        {
            lineRenderersList[i].enabled = false;
        }
        lineRenderersList.Clear();
        ChangedMotherCells.Clear();
        usableMotherCell = null;
    }

    private bool CheckMotherCellList()
    {
        if (ChangedMotherCells.Count == 0) return true;
        else if (ChangedMotherCells[ChangedMotherCells.Count - 1].CompareTag(Tags.Player.ToString())) return true;
        else if (!ChangedMotherCells[ChangedMotherCells.Count - 1].CompareTag(Tags.Player.ToString()))
        {
            ChangedMotherCells.Remove(ChangedMotherCells[ChangedMotherCells.Count - 1]);
            return true;
        }
        else return false;
    }

    private void AddMotherCell(Collider2D collider)
    {
        if ((!ChangedMotherCells.Contains(collider.gameObject)))
        {
            ChangedMotherCells.Add(collider.gameObject);
        }

        usableMotherCell = collider.gameObject;
        lineRenderersList.Clear();

        for (int i = 0; i < ChangedMotherCells.Count; i++)
        {
            LineRenderer lineRenderer = ChangedMotherCells[i].GetComponent<LineRenderer>();
            lineRenderersList.Add(lineRenderer);
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, ChangedMotherCells[i].transform.position);
            lineRenderer.SetPosition(1, usableMotherCell.transform.position);
        }
    }
}

