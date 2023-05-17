using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Text.Json;

public class Word
{
    public Word(string? en, string? az)
    {
        En = en;
        Az = az;
    }

    public string? En { get; set; }
    public string? Az { get; set; }

    public override string ToString()
    {
        return $"{En}={Az}";
    }
}
public class Translatior
{

    public static List<Word>? words = new List<Word>();

    string jsonFilePath = @"Luget2.json";
    public Translatior()
    {
        AddNewWord("way", "yol");
    }
    public void ShowAllWords()
    {
        foreach (var item in Translatior.words)
        {
            Console.WriteLine($"\n{item}");
        }

    }
    public void CreateJson()
    {
        var options = new JsonSerializerOptions() { WriteIndented = true };
        var jsonseri = JsonSerializer.Serialize(words, options);
        string jsonString = File.ReadAllText(jsonFilePath);
        words = JsonSerializer.Deserialize<List<Word>>(jsonString);
        File.WriteAllText(jsonFilePath, jsonseri);
    }
    public static bool IsJsonFileEmpty(string filePath)
    {
        string fileContent = File.ReadAllText(filePath);
        return string.IsNullOrWhiteSpace(fileContent);
    }
    public void AddNewWord(string? EnglishWord, string? translationWord)
    {
        words.Add(new Word(EnglishWord, translationWord));
        CreateJson();
    }
    public void AddAzNewWord(string? AzWord)
    {
        Console.Write("Word did not found write its translation to add to list.");
        string? translationWord = Console.ReadLine();
        words.Add(new Word(translationWord, AzWord));
        CreateJson();
    }
    public void AddNewWord(string? EnglishWord)
    {
        Console.Write("Word did not found write its translation to add to list.");
        string? translationWord = Console.ReadLine();
        words.Add(new Word(EnglishWord, translationWord));
        CreateJson();
    }
    public List<Word> getRandomWords()
    {
        List<Word> tempwords = new List<Word> { };
        Random random = new();
        for (int i = 0; i < 5; i++)
        {
            tempwords.Add(words[random.Next(0, words.Count - 1)]);
        }
        return tempwords;
    }

    public void quizz()
    {
        List<Word> quizwords = getRandomWords();
        int correct = 0;
        int wrongs = 0;
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"{words[i].En}==?");
            string answer = Console.ReadLine();
            if (words[i].Az == answer)
            {
                ++correct;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("duzdu");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                ++wrongs;
                Console.WriteLine("sehvdi");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

    }

    private Word? FindWord(string? word)
    {
        string jsonString = File.ReadAllText(jsonFilePath);
        words = JsonSerializer.Deserialize<List<Word>>(jsonString);
        for (int i = 0; i < words.Count; i++)
        {
            if (word == words[i].En)
            {
                return words[i];
            }
        }
        for (int i = 0; i < words.Count; i++)
        {
            if (word == words[i].Az)
            {
                return words[i];
            }
        }
        return null;
    }
    public void GetEnTranslation()
    {
        try
        {
            Console.Write("Write some english word with first case upper:");
            string? word = Console.ReadLine();

            Word? tempword = FindWord(word);
            if (tempword == null) AddNewWord(word);
            else Console.WriteLine(tempword);
        }
        catch (Exception)
        {
            GetEnTranslation();
        }
    }
    public void GetAzTranslation()
    {
        try
        {
            Console.Write("Write some Az dilinde word :");
            string? word = Console.ReadLine();

            Word? tempword = FindWord(word);
            if (tempword == null) AddAzNewWord(word);
            else Console.WriteLine(tempword);
        }
        catch (Exception)
        {
            GetEnTranslation();
        }
    }
    public void start()
    {
        try
        {
            Console.WriteLine("Choice:\n1:En to Az\n2:Az to En\nQuizz");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1: GetEnTranslation(); break;
                case 2: GetAzTranslation(); break;
                case 3: quizz(); break;

            }

        }
        catch (Exception ex)
        {

            start();
            Console.WriteLine(ex.Message);
        }
        start();
    }
    public object this[int index]
    {
        get { return words[index]; }
    }
    public object this[string word]
    {
        get
        {
            try
            {
                if (FindWord(word) != null) { return FindWord(word); }
                else throw new Exception("Word did not found!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }



}


class Program
{
    static void Main(string[] args)
    {
        Translatior translatior = new();
        translatior.AddNewWord("salam", "hello");
        translatior.start();
        //string jsonFilePath = @"C:\Users\Crash\source\repos\Operator OverLoading\Operator OverLoading\tsconfig1.json";
        //var options = new JsonSerializerOptions();
        //options.WriteIndented = true;
        //var jsonStr = JsonSerializer.Serialize(words, options);
        //File.WriteAllText(jsonFilePath, jsonStr);
        //var readStr = File.ReadAllText(jsonFilePath);
        //var readCars = JsonSerializer.Deserialize<List<Word>>(readStr);

        //foreach (var item in readCars)
        //{
        //    Console.WriteLine(item);
        //}

        //// Json file-a listdeki lugeti qoyub sonra o fileden yeni bir liste qaytarmaq
        //string SerializedJson = JsonSerializer.Serialize(words);
        //File.WriteAllText(jsonFilePath, SerializedJson);
        //var deserialize = JsonSerializer.Deserialize<List<Word>>(SerializedJson);
        //foreach (Word item in deserialize)
        //{
        //    Console.WriteLine(item);
        //}

        ////Hazir json File-daki textleri goturub yeni bir liste gondermek
    }
}