using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class JSONLoader : MonoBehaviour
{
    public Text title;
    public GridLayoutGroup grid;
    public GameObject fieldPrefab;
    private string jsonPath;
    private TableContent content;

    private void Awake()
    {
        jsonPath = Application.streamingAssetsPath + "/JsonChallenge.json";
    }

    private void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        using (StreamReader stream = new StreamReader(jsonPath))
        {
            string json = stream.ReadToEnd();
            content = JsonUtility.FromJson<TableContent>(json);
        }
        ShowDataOnUI();
    }

    private void ShowDataOnUI()
    {
        // Load the title 
        title.text = content.Title;
        // Constraint the grid for data
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = content.ColumnHeaders.Length;
        // Load the headers of the file
        foreach (var header in content.ColumnHeaders)
        {
            GameObject field = Instantiate(fieldPrefab);
            field.transform.parent = grid.transform;
            Text text = field.GetComponent<Text>();
            text.text = header;
            text.fontStyle = FontStyle.Bold;
        }
        // Load the members data
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
}
