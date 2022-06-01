using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NewFrontData;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RandomManager : MonoBehaviour
{
    public int testNum;
    public int aiNum;
    private int nowNum = 0;
    
    public SceneImporter si;
    public bool guessOn;

    private Transform debugWindow;
    public GameObject generateContent;

    private List<string> humanList;
    private List<string> aiList;
    
    string humanPath = $@"{Config.sourcePath}json\";
    string aiPath = $@"{Config.sourcePath}aijson\";
    
    private List<string> jsonNameList;
    private List<int> resultList;



    // Start is called before the first frame update
    void Start()
    {
        humanList = getFlieList(humanPath);
        aiList = getFlieList(aiPath);
        
        generateContent.SetActive(!guessOn);
        reset();
        Debug.Log("Reset Over");
    }

    void reset()
    {
        resultList = new List<int>();
        jsonNameList = new List<string>();
        List<int> humanIdList = getRandList(testNum - aiNum,humanList.Count);
        List<int> aiIdList = getRandList(aiNum,aiList.Count);

        
        for (int i = 0; i < testNum; i++)
        {
            if (i<aiNum)
            {
                jsonNameList.Add("a^"+aiList[aiIdList[i]]);
            }
            else
            {
                jsonNameList.Add("h^"+humanList[humanIdList[i-aiNum]]);
            }
            
        }

        jsonNameList = Outoforder(jsonNameList);
        si.Generate(getJson(nowNum++));
        SceneBuilder.NewRoomBuild(si.data,si.avaRoom[0],true);

        // while (jsonNameList.Count<testNum)
        // {
        //     float rand = Random.Range(0f,1);
        //     if (rand < ((float)aiNum / (float)testNum)) //Add Ai
        //     {
        //         if (aiId < aiIdList.Count)
        //         {if (!jsonNameList.Contains("a_"+aiList[aiIdList[aiId]]))
        //         {
        //             jsonNameList.Add("a_"+aiList[aiIdList[aiId]]);
        //             aiId++;
        //         }}
        //     }
        //     else //Add human
        //     {
        //         if(humanId<humanIdList.Count){if (!jsonNameList.Contains("h_"+humanList[humanIdList[humanId]]))
        //         {
        //             jsonNameList.Add("h_"+humanList[humanIdList[aiId]]);
        //             humanId++;
        //         }}
        //     }
        // }

    }
    
    public void setValue(int type)
    {
        resultList.Add(type);
        if (nowNum< testNum)
        {
            Debug.Log("now nowNum"+nowNum);
            si.Generate(getJson(nowNum));
            SceneBuilder.NewRoomBuild(si.data,si.avaRoom[0],true);
            Debug.Log("type"+type);
            nowNum++;
        }
        else
        {
            evaluateOver();
        }

    }

    private string getJson(int index)
    {
        string type = jsonNameList[index].Split('^')[0];
        string dic = (type == "h") ? "json" : "aijson";
        Debug.Log("generating "+ type);
        string path = $@"{Config.sourcePath}{dic}\{jsonNameList[index].Split('^')[1]}";
        return File.ReadAllText(path);;
    }
    
    public List<string> getFlieList(string path)
    {
        List<string> nameList = new List<string>();
        if (Directory.Exists(path))
        {
            DirectoryInfo direction = new DirectoryInfo(path);
            FileInfo[] files = direction.GetFiles("*");
            
            for (int i = 0; i < files.Length; i++)
            {
                nameList.Add(files[i].Name);
            }
        }

        return nameList;
    }

    private List<int> getRandList(int count,int range)
    {
        List<int> rangeList = new List<int>();
        while (rangeList.Count < count)
        {
            int temp_nums = Random.Range(0, range);
            #region 判断列表中是否有此次生成的随机数，没有则把temp_nums加入列表，否则重新生成            
            if (!rangeList.Contains(temp_nums))
            {
                rangeList.Add(temp_nums);
            }
            else
            {
                continue;
            }
            #endregion
        }

        return rangeList;
    }

    private void evaluateOver()
    {
        writeLog();
    }
    
    public List<T> Outoforder<T>(List<T> bag)
    {
        int index = 0;
        T temp;
        for (int i = 0; i < bag.Count; i++)
        {
            index = Random.Range(0, bag.Count - 1);
            if (index != i)
            {
                temp = bag[i];
                bag[i] = bag[index];
                bag[index] = temp;
            }
        }
        return bag;
    }

    private void writeLog()
    {
        string path = $"{Config.logPath}/{(System.DateTime.Now.Hour*10000 + System.DateTime.Now.Minute*100 + System.DateTime.Now.Second)}.txt";

        StringBuilder line1 = new StringBuilder();
        StringBuilder line2 = new StringBuilder();
        StringBuilder line3 = new StringBuilder();

        line1.Append("Index\t");
        line2.Append("Should\t");
        line3.Append("Guess\t");
        
        for (int i = 0; i < testNum; i++)
        {
            string index = $"{i}";
            string guess = resultList[i] == 0 ? "Human": 
                                                "AI   ";
            string should = jsonNameList[i].Split('^')[0] == "h"?
                                                "Human": 
                                                "AI   ";
            line1.Append(index).Append("\t||");
            line2.Append(should).Append("\t||");
            line3.Append(guess).Append("\t||");

        }

        string content = line1.Append("\n").ToString() + line2.Append("\n").ToString() + line3.Append("\n").ToString();
        using (StreamWriter sr = new StreamWriter(path))
        {
            sr.WriteLine(content);
            sr.Close();
            sr.Dispose();
            Debug.Log(content);
        }
    }
    
}
