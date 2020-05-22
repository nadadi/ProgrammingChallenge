using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class JSONLoader : MonoBehaviour
{
    #region CLASS_VARIABLES
    public GameObject panel;
    public Text title;
    public GameObject fieldPrefab;
    private string jsonPath;
    private TableContent content;
    private GameObject grid;
    private GridLayoutGroup gridLayout;
    #endregion

    #region UNITY_METHODS
    private void Awake()
    {
        jsonPath = Application.streamingAssetsPath + "/JsonChallenge.json";
    }

    private void Start()
    {
        LoadData();
    }
    #endregion

    #region CLASS_METHODS
    public void LoadData()
    {
        using (StreamReader stream = new StreamReader(jsonPath))
        {
            string json = stream.ReadToEnd();
            content = JsonUtility.FromJson<TableContent>(json);
        }

        if (content != null)
        {
            CleanGridLayout();
            ShowDataOnUI();
        }
    }

    private void ShowDataOnUI()
    {
        // Load the title 
        title.text = content.Title;
        // Creates grid, adding a grid layout and constraint for data
        CreateGrid();

        // Load the headers of the file
        AssignHeaders();

        // Load the members data
        LoadContent();
    }

    private void CleanGridLayout()
    {
        Destroy(grid);
    }

    private void CreateGrid()
    {
        grid = new GameObject("Grid");
        grid.transform.parent = panel.transform;
        RectTransform rectTransform = grid.AddComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(0f, -0.0001220703f);
        rectTransform.offsetMax = new Vector2(0f, -1.924133f);
        rectTransform.anchorMin = new Vector2(0f, 0f);
        rectTransform.anchorMax = new Vector2(1f, 0.881f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        gridLayout = grid.AddComponent<GridLayoutGroup>();
        gridLayout.padding.left = 100;
        gridLayout.padding.right = 100;
        gridLayout.cellSize = new Vector2(100, 50);
        gridLayout.startCorner = GridLayoutGroup.Corner.UpperLeft;
        gridLayout.startAxis = GridLayoutGroup.Axis.Horizontal;
        gridLayout.childAlignment = TextAnchor.UpperLeft;
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = content.ColumnHeaders.Length;
    }

    private void AssignHeaders()
    {
        foreach (var header in content.ColumnHeaders)
        {
            GameObject field = Instantiate(fieldPrefab);
            field.transform.parent = grid.transform;
            Text text = field.GetComponent<Text>();
            text.text = header;
            text.fontStyle = FontStyle.Bold;
        }
    }

    private void LoadContent()
    {
        foreach (var data in content.Data)
        {
            GameObject id = Instantiate(fieldPrefab);
            id.transform.parent = grid.transform;
            id.GetComponent<Text>().text = data.ID;

            GameObject name = Instantiate(fieldPrefab);
            name.transform.parent = grid.transform;
            name.GetComponent<Text>().text = data.Name;

            GameObject role = Instantiate(fieldPrefab);
            role.transform.parent = grid.transform;
            role.GetComponent<Text>().text = data.Role;

            GameObject nickname = Instantiate(fieldPrefab);
            nickname.transform.parent = grid.transform;
            nickname.GetComponent<Text>().text = data.Nickname;
        }
    }
    #endregion
}
