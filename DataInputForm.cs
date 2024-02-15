using System.Text;
using ComEngineers.API.Commands;
using ComEngineers.API.Data;
using ComEngineers.API.Models;
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
        var lines = content.Split(",,,,,,");
        lines = lines.Skip(1).SkipLast(1).ToArray();
        var sessionEntries = new List<Session>();
        var accelerometerEntries = new List<Accelerometer>();
        var gyroscopeEntries = new List<Gyroscope>();
        var heartRateEntries = new List<HeartRate>();
        var temperatureEntries = new List<Temperature>();
        var session = new Session
        {
            TimeCode = DateTime.Now
        };
        sessionEntries.Add(session);
        foreach (var line in lines) // 50ms
        {
            // Data  = PulseData*100, ax, ay, az, yaw, pitch, roll, temperature
            var values = line.Split(",");

            accelerometerEntries.Add(new Accelerometer
            {
                Session = session,
                TimeCode = DateTime.Now,
                XValue = float.Parse(values[1]),
                YValue = float.Parse(values[2]),
                ZValue = float.Parse(values[3])
            });
            gyroscopeEntries.Add(new Gyroscope
            {
                Session = session,
                TimeCode = DateTime.Now,
                YAxis = float.Parse(values[4]),
                XAxis = float.Parse(values[5]),
                ZAxis = float.Parse(values[6])
            });
            heartRateEntries.Add(new HeartRate
            {
                Session = session,
                TimeCode = DateTime.Now,
                Value = float.Parse(values[0])
            });
            temperatureEntries.Add(new Temperature
            {
                Session = session,
                TimeCode = DateTime.Now,
                Value = float.Parse(values[7])
            });
        }

        var confirmationMessage = "Upload Complete!\nDo you wish you store this data?";
        var confirmationCaption = "Data Storage Confirmation";

        var confirmResult = MessageBox.Show(confirmationMessage, confirmationCaption, MessageBoxButtons.YesNo);

        if (confirmResult != DialogResult.Yes) return;

        SessionData.AddData(Context, sessionEntries);
        AccelerometerData.AddData(Context, accelerometerEntries);
        GyroscopeData.AddData(Context, gyroscopeEntries);
        HeartRateData.AddData(Context, heartRateEntries);
        TemperatureData.AddData(Context, temperatureEntries);
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