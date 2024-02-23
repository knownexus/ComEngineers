using System.Text;
using ComEngineers.API.Commands;
using ComEngineers.API.Data;
using ComEngineers.API.Models;
using ComEngineers.DataProcessing;
using TypesOfData = ComEngineers.API.Commands.TypesOfData;

namespace ComEngineers;

public partial class DataInputForm : Form
{
    public readonly ComEngineersContext Context;
    private int selectedId;

    public DataInputForm(ComEngineersContext context)
    {
        InitializeComponent();
        Context = context;
        TypesOfData.AddData(Context);

        foreach (var id in SessionData.GetSessionIds(Context)) comboBox1.Items.Add(id);
    }


    private void Upload_Button_Click(object sender, EventArgs e)
    {
        var dialog = new OpenFileDialog();
        dialog.Filter = "CSV Files | *.csv"; // file types, that will be allowed to upload
        dialog.Multiselect = false;
        if (dialog.ShowDialog() != DialogResult.OK) return;

        var path = dialog.FileName;
        using var reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding());

        var content = reader.ReadToEnd();
        ParseData(content);
    }

    private void label1_Click(object sender, EventArgs e)
    {
    }

    private void ParseData(string content)
    {
        var sessionEntries = new List<Session>();
       
        var session = new Session
        {
            TimeCode = DateTime.Now
        };
        sessionEntries.Add(session);

        var data = DataInput.ProcessData(content, session);

        const string confirmationCaption = "Data Storage Confirmation";
        const string confirmationMessage = "Upload Complete!\nDo you wish you store this data?";

        var confirmResult = MessageBox.Show(confirmationMessage, confirmationCaption, MessageBoxButtons.YesNo);

        if (confirmResult != DialogResult.Yes) return;

        DataInput.AddProcessedData(Context, sessionEntries, data);
    }

    private void RequestLatest_Click(object sender, EventArgs e)
    {
        var session = SessionData.GetLatestSession(Context);
        SessionId_Label.Text = session.Id.ToString();
        //session.
    }

    private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedId = int.Parse(comboBox1.SelectedItem?.ToString() ?? "-1");
    }

    private void LoadSessionButton_Click(object sender, EventArgs e)
    {
        if (selectedId == 0)
            // Don't accept just pressing button without input
            return;

        var session = SessionData.GetSessionId(Context, selectedId);
        SessionId_Label.Text = session.ToString();
    }

    private void DisplayDataButton_Click(object sender, EventArgs e)
    {
        (new DataDisplayForm(this)).Show(); this.Hide();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
    }
}