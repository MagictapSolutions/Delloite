using System;
using System.Net.Sockets;
using System.Text;
using TMPro;
using UnityEngine;

public class TCPConnector : MonoBehaviour
{
    public TMP_InputField TCPAdress;

    private TcpClient tcpClient;
    private const int DefaultPort = 12345;
    int a = 0;// Set your default port here

    void Start()
    {
        // Initialize if needed
    }

    void Update()
    {
        // Handle updates if necessary
    }

    public void OnConnect()
    {
        // Get the address from the input field
        string address = TCPAdress.text;

        if (string.IsNullOrEmpty(address))
        {
            Debug.LogError("Address cannot be empty.");
            return;
        }

        // Connect to the TCP server
        try
        {
            // Create a new TcpClient instance
            tcpClient = new TcpClient();

            // Connect to the server using the default port
            tcpClient.Connect(address, DefaultPort);

            Debug.Log($"Connected to {address}:{DefaultPort}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error connecting to server: {ex.Message}");
        }
    }

    private void OnApplicationQuit()
    {
        // Ensure we close the connection when the application exits
        if (tcpClient != null && tcpClient.Connected)
        {
            tcpClient.Close();
        }
    }

    public void SendCountryCode(int countryCode)
    {
        if (tcpClient == null || !tcpClient.Connected)
        {
            Debug.LogError("Not connected to the server.");
            return;
        }

        try
        {
            NetworkStream stream = tcpClient.GetStream();
            byte[] data = Encoding.ASCII.GetBytes(countryCode.ToString());
            stream.Write(data, 0, data.Length);
            Debug.Log($"Sent country code {countryCode} to server.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error sending data: {ex.Message}");
        }
    }

    public void SendCountryInsideCode(string page)
    {
        if (tcpClient == null || !tcpClient.Connected)
        {
            Debug.LogError("Not connected to the server.");
            return;
        }

        try
        {
            NetworkStream stream = tcpClient.GetStream();

            // Convert the 'page' parameter (country code) to a byte array
            byte[] data = Encoding.ASCII.GetBytes(page);

            // Send the data to the server
            stream.Write(data, 0, data.Length);

            Debug.Log($"Sent country code '{page}' to server.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error sending data: {ex.Message}");
        }
    }


    public void Scot() { SendCountryCode(2); }
    public void Ire() { SendCountryCode(4); }
    public void Can() { SendCountryCode(6); }
    public void India() { SendCountryCode(1); }
    public void Australia() { SendCountryCode(2); }
    public void Bangladesh() { SendCountryCode(3); }
    public void Canada() { SendCountryCode(4); }
    public void China() { SendCountryCode(5); }
    public void Germany() { SendCountryCode(6); }
    public void Ireland() { SendCountryCode(7); }
    public void Qatar() { SendCountryCode(8); }
    public void NetherLand() { SendCountryCode(9); }
    public void Nepal() { SendCountryCode(10); }
    public void NewZeaLand() { SendCountryCode(11); } // Note: Scotland shares the UK code
    public void ScotLand() { SendCountryCode(12); }
    public void SaudiArabia() { SendCountryCode(13); }
    public void UK() { SendCountryCode(14); }
    public void US() { SendCountryCode(15); }
    public void Deloitte() { SendCountryCode(16); }
    public void India1()
    {
        if (a < 4)
        {
            a++;
            SendCountryInsideCode(a.ToString());
        }
       
    }

    public void India2()
    {
        if (a > 0)
        {
            a--;
        }
        SendCountryInsideCode(a.ToString());
    }

    public void Indiastate1()
    {
        if (a < 3)
        {
            a++;
            SendCountryInsideCode(a.ToString());
        }

    }

    public void Indiastate2()
    {
        if (a > 0)
        {
            a--;
        }
        SendCountryInsideCode(a.ToString());
    }
    public void state()
    {
        SendCountryInsideCode("s");
    }
    public void central()
    {
        SendCountryInsideCode("c");
    }
    public void India3()
    {
        SendCountryInsideCode("16");
    }
    public void OnHome()
    {
        SendCountryInsideCode("h");
    }
    public void page1()
    {
        SendCountryInsideCode("a");
    }
    public void page2()
    {
        SendCountryInsideCode("b");
    }
    public void page3()
    {
        SendCountryInsideCode("c");
    }
    public void page4()
    {
        SendCountryInsideCode("d");
    }
    public void page5()
    {
        SendCountryInsideCode("e");
    }
    public void page6()
    {
        SendCountryInsideCode("f");
    }
}
