using System;
using System.Diagnostics;
using System.IO;

public class Program
{
    public static void Main()
    {
        string path = "D:\\Steam\\text.txt"; //absolute path
        //string path = "text.txt"; //path
        StreamReader s = new StreamReader(path);
        string text = s.ReadToEnd() + " ";
        Console.WriteLine(text);

        string[] prepositions = new string[] {"in","for", "at","on","to"};
        string[] wordsWithoutRep;
        int[] countWithoutRep;
        
        int count = 0;
        int j = 0;// for MainCycle
        int k = 0;// for MainCycle
        
        int i = 0; //index
        string[] words; // all words
        Count: //Count of words
        if (i == text.Length - 1)
        {
            count++;
            i = 0;
            words = new string[count];
            goto MainCycle;
        }
        if (text[i] == ' ' || text[i] == '\n')
        { 
            count++;
        }
        i++;
        goto Count;
        
        MainCycle:
            if (text[i] == ' ' || text[i] == '\n') // added i
            {
                string word = "";
                Word:
                    if (k != i)
                    {
                        if (text[k] != '\r')
                        {
                            if (text[k] > 64 && text[k] < 91)
                            {
                                char c = (char)(text[k] + 32);
                                word += c;
                            }
                            else
                            {
                                word += text[k];
                            }
                        }
                    }
                    else // всё работало без проверки
                    {
                        int p = 0;
                        CheckPreposition:
                            if (p >= prepositions.Length)
                                goto ExitCheck;
                            if (word == prepositions[p])
                            {
                                //j++;
                                k++;
                                goto ExitIf;
                            }
                            p++;
                        goto CheckPreposition;
                        ExitCheck:
                        words[j] = word;
                        j++;
                        k++;
                        goto ExitIf;
                    }
                    k++;
                goto Word;
            }
            ExitIf:
            if (i == text.Length - 1)
            {
                i = 0;
                j = 0;
                k = 0;
                wordsWithoutRep = new string[count];
                countWithoutRep = new int[count];
                goto GetCount1;
            }
            i++;
                
        goto MainCycle;
        
        GetCount1:
            if (i > count - 1)
            {
                i = 1;
                j = 0;
                goto Buble1;
            }
            j = 0;
            GetCount2:
                if(j > count - 1) // leave circle
                    goto ExitCount2;
                if (words[i] == wordsWithoutRep[j])
                {
                    countWithoutRep[j]++;
                    j++;
                    goto ExitCount2;
                }
                else if(wordsWithoutRep[j] == null)
                {
                    wordsWithoutRep[k] = words[i];
                    countWithoutRep[k]++;
                    k++;
                    goto ExitCount2;
                }
                j++;
            goto GetCount2;
            
            ExitCount2:
            i++;
        goto GetCount1;
        
        Buble1:
            if (i >= k)
            {
                i = 0;
                goto Finish;  
            }

            j = 0;
            Buble2: //cycle
                if(j >= k - i)
                    goto ExitBuble;
                if (countWithoutRep[j] < countWithoutRep[j + 1])
                {
                    //swap counts
                    int temp = countWithoutRep[j];
                    countWithoutRep[j] = countWithoutRep[j + 1];
                    countWithoutRep[j + 1] = temp;
                    //swap words
                    string temp1 = wordsWithoutRep[j];
                    wordsWithoutRep[j] = wordsWithoutRep[j + 1];
                    wordsWithoutRep[j + 1] = temp1;
                }
                j++;
            goto Buble2;
            
            ExitBuble:
            i++;
        goto Buble1;

        Finish:
            if(wordsWithoutRep[i] == null)
                goto Stop;
            Console.WriteLine(wordsWithoutRep[i] + " - " + countWithoutRep[i]);
            i++;
        goto Finish;
        
        Stop: ;
    }

}