using System;
using System.Diagnostics;
using System.IO;

public class Program
{
    public static void Main()
    {
        string path = "D:\\Steam\\text2.txt"; //absolute path
        StreamReader s = new StreamReader(path);
        string text = s.ReadToEnd();
        //Console.WriteLine(text);
        int i = 0;
        int j = 0;
        int k = 0;
        int l = 0; // letter
        int p = 0;
        int count = 1;
        int limitPage = 45; // count of lines in a page
        string newText = "";
        string signs = ".,\"“”!?;";
        string[] resultWords;
       // int wordsNumberInPage = 0;
        string[] lines;
        string[] pages;
        string[,] words;
        DeleteSign:
            if (i >= text.Length)
            {
                i = 0;
                j = 0;
                lines = new string[count];
                InitLines:
                    if (i >= count)
                    {
                        i = 0;
                        goto ExitInitPages;
                    }
                    lines[i] = "";
                    i++;
                goto InitLines;
                ExitInitPages:
                goto Lines;
            }

            if (text[i] == '\n')
            {
                count++;
            }
            
            j = 0;
            DeleteSign2:
                if (j >= signs.Length)
                {
                    newText += text[i];
                    goto ExitDeleteSign2;
                }
                if (text[i] == signs[j])
                {
                    //newText += "";
                    goto ExitDeleteSign2;
                }
                j++;
            goto DeleteSign2;
            ExitDeleteSign2:
            i++;
        goto DeleteSign;
        
        Lines:
            if (i  >= newText.Length)
            {
                i = 0;
                j = 0;
                pages = new string[lines.Length/limitPage + 1 ];
                goto Pages;
            }
            if (newText[i] != '\r' && newText[i] != '\n')
            {
                lines[j] += newText[i];
                i++;
            }
            else
            {
                j++;
                i += 2;
            }
        goto Lines;
        
        Pages:
            if (i >= pages.Length || j >= lines.Length)
            {
                words = new string[pages.Length,5000]; // 5k words - 1 page
                i = 0;
                j = 0;
                l = 0;
                goto Words;
            }

            if (j % limitPage != 0 || j == 0)
            {
                if(lines[j] != "")
                    pages[i] += lines[j] + " "; 
                j++;
            }
            else
            {
                i++;
                if(lines[j] != "")
                    pages[i] += lines[j] + " ";
                j++;
            }
            
            //j++;
        goto Pages;

        Words:
            if (i >= pages.Length)
            {
                i = 0;
                j = 0;
                k = 0;
                l = 0;
                goto DeleteRep; 
            }
            Words1:
            if (pages[i][l] == ' ') 
            {
                string word = "";
                Word:
                if (k != l)
                {
                    if (pages[i][k] != '\r')
                    {
                        if (pages[i][k] > 64 && pages[i][k] < 91)
                        {
                            char c = (char)(pages[i][k] + 32);
                            word += c;
                        }
                        else
                        {
                            word += pages[i][k];
                        }
                    }
                }
                else // всё работало без проверки
                {
                    words[i,j] = word;
                    j++;
                    k++;
                    goto ExitIf;
                }
                k++;
                goto Word;
            }
            ExitIf:
            if (l == pages[i].Length - 1)
            {
                j = 0;
                k = 0;
                l = 0;
                goto ExitWords1;
            }
            l++;
            goto Words1;
            ExitWords1:
            i++;
            goto Words;
        
        DeleteRep: // page cycle
            if (i >= pages.Length)
            {
                i = 0;
                j = 0;
                k = 0;
                l = 0;
                resultWords = new string[pages.Length * 5000];
                goto ResultPages;
            }
            DeleteRep1://word cycle
                if (words[i,j] == null)
                {
                    j = 0;
                    k = 0;
                    goto ExitDeleteRep1;
                }
                string currWord = words[i, j];
                DeleteRep2: //check word cycle
                    if (words[i,k] == null)
                    {
                        k = 0; // need to rest
                        goto ExitDeleteRep2;
                    }

                    if (currWord == words[i, k] && j != k)
                    {
                        words[i, k] = "";
                    }
    
                    k++; //check word
                goto DeleteRep2;
                
                ExitDeleteRep2: // goto next word
                
                j++; //word
            goto DeleteRep1;
            
            ExitDeleteRep1: // GOTO next page
            i++; //page
        goto DeleteRep;
        
        ResultPages: // page
           // Console.WriteLine(i);
            if (i >= pages.Length)
            {
                i = 0;
                j = 0;
                k = 0;
                goto Check100;
            }
            ResultPages1: // word
               // Console.WriteLine(j);
                if (words[i,j] == null)
                {
                    j = 0;
                    l = 0;
                    k = 0;
                    goto ExitResultPages1;
                }
                if (words[i, j] == "")
                {
                    j++;
                    goto ResultPages1;
                }
                string word1 = words[i, j]; // page-word
                resultWords[p] = word1 + " - " + (i+1) + ", ";
                ResultPages2:
                   // Console.WriteLine(l);
                    if (l >= pages.Length)
                    {
                        l = 0;
                        k = 0;
                        goto ExitResultPages2;
                    }

                    if (l == i)
                    {
                        l++;
                        goto ResultPages2;
                    }
                    
                    ResultPages3:
                       // Console.WriteLine(k);
                        if (words[l, k] == null)
                        {
                            k = 0;
                            goto ExitResultPages3;
                        }

                        if (words[l,k] == "")
                        {
                            k++;
                            goto ResultPages3;
                        }

                        if (word1 == words[l, k] && l != i)
                        {
                            words[l, k] = "";
                            resultWords[p] += (l+1) + ", ";
                        }

                        k++;
                    goto ResultPages3;
                    
                    ExitResultPages3:


                    l++;
                goto ResultPages2;

                ExitResultPages2:
                j++;
                p++; // ??????
            goto ResultPages1;
            
            ExitResultPages1: // goto next page
            i++;
        goto ResultPages;
        
        Check100:
            if (resultWords[i] == null)
            {
                i = 0;
                j = 0;
                goto Sort;
            }
            Check1001:
                if (j >= resultWords[i].Length)
                {
                    j = 0;
                    goto Exit;
                }
                if (resultWords[i][j] == ',')
                {
                    k++;
                }
                j++;
            goto Check1001;
            
            Exit:
            if (k >= 100)
            {
                resultWords[i] = "";
               // k = 0;
            }

            k = 0;
            i++;
        goto Check100; 
        
        Sort:
            if (resultWords[i] == null)
            {
                i = 0;
                j = 0;
                goto Finish;
            }
            j = i + 1;
            Sort1:
                
                if (resultWords[j] == null)
                {
                    j = 0;
                    goto ExitSort1;
                }

                if (resultWords[i][0] > resultWords[j][0])
                {
                    string temp = resultWords[i];
                    resultWords[i] = resultWords[j];
                    resultWords[j] = temp;
                    j++;
                    goto Sort;
                }

                if (resultWords[i][0] == resultWords[j][0] && resultWords[i][1] > resultWords[j][1])
                {
                    string temp = resultWords[i];
                    resultWords[i] = resultWords[j];
                    resultWords[j] = temp;
                    j++;
                    goto Sort;
                }
                j++;
                goto Sort1;
            
            ExitSort1:
            i++;
        goto Sort;
        
        Finish:
        if (resultWords[i] == null)
        {
            i = 0;
            j = 0;
            goto Next;
        }
        if(resultWords[i] != "")
            Console.WriteLine(resultWords[i]);
        i++;
        goto Finish;
        Next: ;
    }
}