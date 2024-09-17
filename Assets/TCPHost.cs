using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class TCPHost : MonoBehaviour
{
    private TcpListener tcpListener;
    private TcpClient client;
    private const int Port = 12345; // Default port for the TCP server
    private bool isRunning = false;
    public Sprite[] countries;
    public Sprite[] IndiaState;
    public Sprite[] IndiaCentral;
    int indiaIndex;
    string receivedText = "0";
    int a = 0;

    public GameObject source;
    private float lastReceivedTime;
    private string lastReceivedText = "0"; // Store the last received text
    private float timeoutDuration = 30f; // 30 seconds timeout

    void Start()
    {
        // Start the server when the game starts
        StartServer();
        lastReceivedTime = Time.time; // Initialize the last received time
    }

    void Update()
    {
        // Check if the received text has remained the same for more than 30 seconds
        if (receivedText == lastReceivedText && Time.time - lastReceivedTime > timeoutDuration)
        {
            // Change the sprite to countries[1] if the timeout is reached
            if (source.GetComponent<SpriteRenderer>().sprite != countries[1])
            {
                receivedText = "0";
                source.GetComponent<SpriteRenderer>().sprite = countries[1];
                Debug.Log("Timeout reached with same text, changed sprite to countries[1]");
            }
        }

        // Ensure the server is running and the client is connected
        if (isRunning && client != null && client.Connected)
        {
            // Handle non-numeric receivedText like "s", "c", and "h"
            if (receivedText == "a")
            {
                source.GetComponent<SpriteRenderer>().sprite = countries[2];
            }
            if (receivedText == "b")
            {
                source.GetComponent<SpriteRenderer>().sprite = countries[3];
            }
            if (receivedText == "c")
            {
                source.GetComponent<SpriteRenderer>().sprite = countries[4];
            }
            if (receivedText == "d")
            {
                source.GetComponent<SpriteRenderer>().sprite = countries[5];
            }
            if (receivedText == "e")
            {
                source.GetComponent<SpriteRenderer>().sprite = countries[6];
            }
            if (receivedText == "f")
            {
                source.GetComponent<SpriteRenderer>().sprite = countries[7];
            }
            if (receivedText == "s")
            {
                if (source.GetComponent<SpriteRenderer>().sprite != IndiaState[0])
                {
                    source.GetComponent<SpriteRenderer>().sprite = IndiaState[0];
                    Debug.Log("Set to India State 0");
                }
            }
            else if (receivedText == "k")
            {
                if (source.GetComponent<SpriteRenderer>().sprite != IndiaCentral[0])
                {
                    source.GetComponent<SpriteRenderer>().sprite = IndiaCentral[0];
                    Debug.Log("Set to India Central 0");
                }
            }
            else if (receivedText == "h")
            {
                if (source.GetComponent<SpriteRenderer>().sprite != countries[1])
                {
                    source.GetComponent<SpriteRenderer>().sprite = countries[1];
                    Debug.Log("Set to countries[1]");
                }
            }
            else
            {
                // Handle numeric receivedText for country or India arrays
                int countryIndex;
                if (int.TryParse(receivedText, out countryIndex))
                {
                    if (!IsSourceSpriteEqualToCountryOrIndia())
                    {
                        if (countryIndex < countries.Length)
                        {
                            if (source.GetComponent<SpriteRenderer>().sprite != countries[countryIndex])
                            {
                                source.GetComponent<SpriteRenderer>().sprite = countries[countryIndex];
                                Debug.Log($"Set to country {countryIndex}");
                            }
                        }
                    }
                    else
                    {
                        if (IsSourceEqualToState())
                        {
                            if (countryIndex < IndiaState.Length)
                            {
                                if (source.GetComponent<SpriteRenderer>().sprite != IndiaState[countryIndex])
                                {
                                    source.GetComponent<SpriteRenderer>().sprite = IndiaState[countryIndex];
                                    Debug.Log($"Set to India State {countryIndex}");
                                }
                            }
                        }
                        else
                        {
                            if (countryIndex < IndiaCentral.Length)
                            {
                                if (source.GetComponent<SpriteRenderer>().sprite != IndiaCentral[countryIndex])
                                {
                                    source.GetComponent<SpriteRenderer>().sprite = IndiaCentral[countryIndex];
                                    Debug.Log($"Set to India Central {countryIndex}");
                                }
                            }
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("Invalid numeric input received.");
                }
            }
        }
    }

    private bool IsSourceEqualToState()
    {
        foreach (var s in IndiaState)
        {
            if (source.GetComponent<SpriteRenderer>().sprite == s)
                return true;
        }
        return false;
    }

    private bool IsSourceSpriteEqualToCountryOrIndia()
    {
        foreach (Sprite indiaSprite in IndiaState)
        {
            if (source.GetComponent<SpriteRenderer>().sprite == indiaSprite)
            {
                return true;
            }
        }
        foreach (Sprite indiaSprite in IndiaCentral)
        {
            if (source.GetComponent<SpriteRenderer>().sprite == indiaSprite)
            {
                return true;
            }
        }
        return false;
    }

    private void StartServer()
    {
        try
        {
            tcpListener = new TcpListener(IPAddress.Any, Port);
            tcpListener.Start();
            isRunning = true;
            Debug.Log($"Server started on port {Port}");
            AcceptClientsAsync();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error starting server: {ex.Message}");
        }
    }

    private async void AcceptClientsAsync()
    {
        while (isRunning)
        {
            try
            {
                client = await tcpListener.AcceptTcpClientAsync();
                Debug.Log("Client connected");
                HandleClientAsync(client);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error accepting client: {ex.Message}");
            }
        }
    }

    private async void HandleClientAsync(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead;

        try
        {
            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                string newText = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Debug.Log($"Received data from client: {newText}");

                if (newText != receivedText)
                {
                    // Update received text and reset the timer
                    receivedText = newText;
                    lastReceivedText = newText;
                    lastReceivedTime = Time.time;
                }
                else
                {
                    // Update only the timer if the text hasn't changed
                    lastReceivedTime = Time.time;
                }

                // Echo the data back to the client
                await stream.WriteAsync(buffer, 0, bytesRead);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error handling client: {ex.Message}");
        }
        finally
        {
            stream.Close();
            client.Close();
            Debug.Log("Client disconnected");
        }
    }

    private void OnApplicationQuit()
    {
        isRunning = false;
        if (tcpListener != null)
        {
            tcpListener.Stop();
        }
        if (client != null && client.Connected)
        {
            client.Close();
        }
    }
}
