using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Current;
    private void Awake()
    {
        if (Current == null)
        {
            Current = this;
        }
    }

    public static bool LeftGloveOn = false;
    public static bool RightGloveOn = false;
    public static bool SyringePickedUp = false;
    public static bool SyringeInVial = false;
    public static bool SyringeFilledUp = false;
    public static bool MedicationAdmistered = false;

    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject LeftGlove;
    public GameObject RightGlove;
    public GameObject Syringe;
    public GameObject Vial;
    public GameObject PatientArm;

    [Serializable]
    public class Attempts
    {
        [SerializeField] public int attempts = 1;
    }

    public Attempts attempt = new Attempts();

    public Attempts[] vaccineAdministration = new Attempts[3];

    public string FileName = "DeliverVaccineAttempts";
    public int NumberOfEntriesToSave = 3;

    public delegate void DelegateResetGame();
    public DelegateResetGame delegateResetGame;

    public void PutOnLeftGlove()
    {
        LeftGloveOn = true;
        LeftGlove.SetActive(true);
    }
    public void PutOnRightGlove()
    {
        RightGloveOn = true;
        RightGlove.SetActive(true);
    }
    public void PickUpSyringe()
    {
        if(LeftGloveOn && RightGloveOn)
        {

        }
        else
        {
            ResultsController.Current.WriteText( "Syringe Picked Up Without Gloves On" );
            attempt.attempts++;
        }
    }
    public void PlaceSyringeInVial()
    {
        SyringeInVial = true;
        SyringeFilledUp = true;

    }
    public void PlaceSyringeInPatient()
    {
        if (SyringeFilledUp)
        {
            ResultsController.Current.WriteText( "Completed in " + attempt.attempts + " attempts" );
            SaveAttemptsToJson(FileName, attempt, NumberOfEntriesToSave);
        }
        else
        {
            ResultsController.Current.WriteText( "No Medication In Syringe" );
            attempt.attempts++;
        }
    }
    public void ResetGame()
    {
        LeftGloveOn = false;
        RightGloveOn = false;
        SyringePickedUp = false;
        SyringeInVial = false;
        SyringeFilledUp = false;
        MedicationAdmistered = false;
        LeftGlove.SetActive(false);
        RightGlove.SetActive(false);
        delegateResetGame();
    }
    public void SaveAttemptsToJson(string filename, object data, int numberOfEntries)
    {
        string LoadJson = ReadFromFIle(filename);
        vaccineAdministration = JsonHelper.FromJson<Attempts>(LoadJson);

        for (int i = 1; i < numberOfEntries; i++)
        {
            vaccineAdministration[0] = vaccineAdministration[i - 1];
        }
        vaccineAdministration[numberOfEntries-1] = attempt;

        string json = JsonHelper.ToJson(vaccineAdministration, true);
        WriteToFile(filename, json);
    }
    public void Save(string filename, object data)
    {
        string json = JsonUtility.ToJson(data);
        WriteToFile(filename, json);
    }
    private void WriteToFile(string fileName, string json)
    {
        string path = GetFilePath(fileName);

        File.WriteAllText(path, json);
    }
    private string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }
    private string ReadFromFIle(string fileName)
    {
        string path = GetFilePath(fileName);
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        return null;
    }
}
public class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }
    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}