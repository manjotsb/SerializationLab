using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

public class Event
{
    //Event class with event number and location as attributes.
    public int eventNumber { get; set; }
    public string location { get; set; }

    public Event(int eventNumber, string location)
    {
        this.eventNumber = eventNumber;
        this.location = location;
    }
}

class Program
{
    static void Main(string[] args)
    {
        string eventFilePath = "event.txt";
        string jsonFilePath = "event.json";
        string hackathonFilePath = "hackathon.txt";

        Event event0 = new Event(1, "Calgary");

        //Using serialization to seriealize the object to event.txt
        SerializeEvent(eventFilePath, event0);

        //Deserailizing the file and displaying the values on the console.
        DeserializeEvent(jsonFilePath);

        List<Event> eventList = new List<Event>();
        eventList.Add(event0);
        eventList.Add(new Event(3, "Vancouver"));
        eventList.Add(new Event(4, "Toronto"));
        eventList.Add(new Event(5, "Edmonton"));

        foreach (Event a in eventList)
        {
            SerializeToJson(a, jsonFilePath);
        }

        DeserializeFromJson(jsonFilePath);

        ReadFromFile("hackathon.txt");
    }

    static void SerializeToJson(object obj,string filePath)
    {
        string jsonString = JsonSerializer.Serialize(obj) + "\n";
        File.AppendAllText(filePath, jsonString);
        Console.WriteLine("JSON Serialization Done......");
    }

    static void DeserializeFromJson(string filePath)
    {
        string[] jsonString = File.ReadAllLines(filePath);

        foreach (string str in jsonString)
        {
            Event a = JsonSerializer.Deserialize<Event>(str);
            Console.WriteLine($"Event Number: {a.eventNumber}");
            Console.WriteLine($"Location: {a.location}");
        }

        Console.WriteLine("JSON Deserialization Done......");
    }

    static void SerializeEvent(string jsonPath, Event event1)
    {
        using (StreamWriter stream1 = new StreamWriter(jsonPath))
        {
            stream1.WriteLine(System.Text.Json.JsonSerializer.Serialize(event1));
            Console.WriteLine("Serialization Done.....");
        }
    }

    static void DeserializeEvent(String jsonPath)
    {
        using (StreamReader sr = new StreamReader(jsonPath))
        {
            Event eventDeserialized = JsonSerializer.Deserialize<Event>(sr.ReadToEnd());
            Console.WriteLine(eventDeserialized);
            Console.WriteLine("Deserialization Done.....");
        }
    }
    static void ReadFromFile(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.Write("Hackathon");
        }

        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
            fs.Seek(0, SeekOrigin.Begin);
            int firstChar = fs.ReadByte();
            Console.WriteLine($"First Character : \"{(char)firstChar}\"");
            fs.Seek(fs.Length / 2, SeekOrigin.Begin);
            int middleChar = fs.ReadByte();
            Console.WriteLine($"Second  Character is: \"{(char)middleChar}\"");
            fs.Seek(-1, SeekOrigin.End);
            int lastChar = fs.ReadByte();
            Console.WriteLine($" Last Character: \"{(char)lastChar}\"");
        }
    }
}
