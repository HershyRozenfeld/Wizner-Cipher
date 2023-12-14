namespace Wizner_cipher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n\n" +"------------ Wizner's encryption table -------------"+ "\n\n");
            char[] abcArr = ABC_ARR();
            char[,] matX = WiznerMat(abcArr);
            for (int i = 0; i < matX.GetLength(0); i++)
            {
                for (int j = 0; j < matX.GetLength(1); j++)
                {
                    Console.Write(" " + matX[i, j]);
                }
                Console.WriteLine();
            }
            Console.Write("\n\n");
            var (KeyLengthMess, OnlyLettersMess) = ArrMessKey();//מזמן את הפונקציה הכוללת שני חלקים ונותן למערכת לבדוק בתוך הפונקציה מה סוג המשתנים

            char[] EncrypArr = Encryption(OnlyLettersMess, KeyLengthMess, matX);
            Console.WriteLine("The encrypted letters in Vizner Cipher: " + "\n");
            for (int i = 0; i < EncrypArr.Length; i++)
            {
                Console.Write(EncrypArr[i]);
            }
            Console.Write("\n\n");
            Console.WriteLine("Do you want to decrypt back what you encrypted, or are you interested in decrypting something else?");
            Console.WriteLine("Enter P to decode the previous cipher, or enter O for another cipher:" + "\n");
            string Question = (Console.ReadLine());
            Console.WriteLine();
           
            if (Question == "o" || Question == "O")
            {
                var (KeyLengthMes, Mess) = KeyAndEncryptedMess();
                char[] DecrypArr_2 = Decryption(Mess, KeyLengthMes, matX);
                Console.WriteLine("The decrypted cipher: " + "\n");
                for (int i = 0; i < DecrypArr_2.Length; i++)
                {
                    Console.Write(DecrypArr_2[i]);
                }
            }
            if (Question == "p" || Question == "P")
            {
                char[] DecrypArr = Decryption(EncrypArr, KeyLengthMess, matX);
                Console.WriteLine("The decrypted cipher: " + "\n");
                for (int i = 0; i < DecrypArr.Length; i++)
                {
                    Console.Write(DecrypArr[i]);
                }
            }
            Console.Write("\n\n");
            Console.WriteLine("--------------------------------------------------" + "\n\n");
        }
        static char[] ABC_ARR()//פונקציה שמחזירה מערך של צ'אר עם כל האותיות באנגלית בתוכה
        {
            char[] abcArr = new char[26];
            int temp;
            for (int i = 0; i < 26; i++)
            {
                temp = 'a' + i;
                abcArr[i] = (char)temp;
            }
            return abcArr;
        }
        static char[,] WiznerMat(char[] abcArr)//פונקציה שבונה מטריצה של אותיות בצורת טבלת ויז'נר
        {
            char[,] mat = new char[26, 26];//הגדרת מטריצה עבור טבלת ויז'נר
            int i, j, k = 1, n = 1;

            for (i = 0; i < mat.GetLength(0); i++)
            {
                for (j = 0; j < mat.GetLength(1); j++)
                {
                    if (j + k < mat.GetLength(1))//בודק שסך שני המספרים המועמדים להזרקה במיקום שלהם אינו יוצא מגבולות המטריצה
                    {
                        mat[i, j + k] = abcArr[j];//תפקיד הקיי להשהות את התחלת ההזרקה של האותיות לתא מרוחק יותר  

                    }

                    if (j < k)//במקרה שהג'יי קטן מהקיי אז צריך לבצע הזרקה מסוף המערך שהם בעצם האותיות הסופיות 
                    {
                        mat[i, j] = abcArr[abcArr.Length - n + j];
                    }// תפקידו של האות הן הוא להגדיר מנין תחל היניקה מהמערך בכל שורה במטריצה ובכל ירידת שורה היניקה מתקדמת באות
                }//תפקיד הג'יי להביא את המספרים ממקום ההן עד הסוף כמה שנכנס 
                k++;
                n++;
            }
            return mat;
        }
        static (char[], char[]) ArrMessKey()
        {
            int l = 0, Num = 0;
            Console.WriteLine("Please enter an encryption key (The key is one word): " + "\n");
            string Key = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Please enter the message: " + "\n");
            string Mass = Console.ReadLine();
            Console.WriteLine();
            char[] ArrKey = Key.ToCharArray();
            char[] ArrMess = Mass.ToCharArray();
            for (int i = 0; i < ArrMess.Length; i++)
            {
                if (ArrMess[i] != '\0' && ArrMess[i] != ' ' && ArrMess[i] != ',' && ArrMess[i] != '.')
                //מחפש כמה תאים במערך מלאים בתווים ולא עומדים רקים, והאם אינם תווי רווח
                {
                    Num++;
                }
            }
            char[] KeyLengthMess = new char[Num];//מכיל את גודל מערך ההודעה לא כולל תאים מיותרים ישמש עבור תווי המפתח
            char[] OnlyLettersMess = new char[Num];//מכיל את גודל מערך ההודעה לא כולל תאים מיותרים ישמש עבור תווי ההודעה 
            int k = 0;//משתנה שמטרתו לינוק מהמפתח את התווים שלו ולרוץ איתם פעם אחר פעם כאורך ההודעה 
            for (int i = 0; i < ArrMess.Length; i++)
            {
                if (ArrMess[i] != '\0' && ArrMess[i] != ' ' && ArrMess[i] != ',' && ArrMess[i] != '.')
                //עובר רק על תאים מלאים ולא שעומדים רקים, וגם לא עובר על תווי רווח
                {
                    KeyLengthMess[l] = ArrKey[k];
                    OnlyLettersMess[l] = ArrMess[i];//האיי נותן אפשרות לסנן רק את התווים הרצויים מההודעה 
                    k++;
                    l++;//מטרתו לעלות בלי להתאפס בשונה מחברו הקיי וגם אינו עולה במידה ואינו מתקיים תנאי האיף כך שהאיי אינו יכול להחליפו
                }
                if (k == ArrKey.Length)//איפוס המשתנה בשביל שיחזור לינוק מתחילת המפתח ולא ייצא מגבולתיו
                {
                    k = 0;
                }
            }
            return (KeyLengthMess, OnlyLettersMess);
        }
        static char[] Encryption(char[] OnlyLettersMess, char[] KeyLengthMess, char[,] mat)
        {
            char[] EncrypArr = new char[OnlyLettersMess.Length];//מערך חדש לקליטת התווים המוצפנים בגודל המתאים לפי אורך ההודעה 
            int i = 0, k = 0, temp_i = 0, temp_j = 0;
            bool Find_i = false;
            bool Find_j = false;
            while (k < KeyLengthMess.Length)
            {
                if (OnlyLettersMess[k] == mat[i, 0])//הלולאה מוזיזה רק את מספר השורה של המטריצה ובודקת האם הוא מכיל ערך שווה לתו במערך ההודעה
                {
                    temp_i = i;//אם כן יקבל הטמפ את מיקומו המדויק של התא המשקיף על התא שצריך למצוא אותו לפי מידת ההזחה
                    Find_i = true;
                }
                if (KeyLengthMess[k] == mat[0, i])
                {
                    temp_j = i;
                    Find_j = true;
                }
                i++;
                if (Find_i == true && Find_j == true)
                {
                    EncrypArr[k] = mat[temp_i, temp_j];
                    Find_i = false;
                    Find_j = false;
                    k++;
                    i = 0;
                }
            }
            return EncrypArr;
        }
        static char[] Decryption(char[] EncrypArr, char[] KeyLengthMess, char[,] mat)
        {//בפיענוח המטרה היא לצוד את התו מעמודה 0 שהוא התיו המקורי לפני ההצפנה 
            char[] DecryptionArr = new char[EncrypArr.Length];
            int i = 0, k = 0, j = 0;
            while (k < EncrypArr.Length)//מעבר על כל המערך של התווים המוצפנים
            {
                for (j = 0; j < mat.GetLength(1); j++)//לולאה שתרוץ רק על העמודות (לרוחב המטריצה) בשורה העליונה ותחפש את מיקום המפתח 
                {
                    if (KeyLengthMess[k] == mat[0, j])//הלולאה במקום הכי ראשוני 0,0 בודקת השוואה לתא הראשון במערך המפתח ובסיבוב הבא ובמידה 
                    {//ולא ימצא בסיבוב הבא אותו תא במערך המפתחות יבדק מול תא 0,1 במערך עקב עליית הג'יי באחד
                        for (i = 0; i < mat.GetLength(0); i++)
                        {//לולאה שתרוץ רק על השורות (לגובה המטריצה) בעמודה הספציפית שעמדה בדרישות האיף החיצוני ותחפש את מיקום התיו המוצפן
                            if (EncrypArr[k] == mat[i, j])
                            {
                                DecryptionArr[k] = mat[i, 0];
                                k++;
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            return DecryptionArr;
        }
        static (char[], char[]) KeyAndEncryptedMess()
        {
            int k = 0, l = 0;
            string EncryptedMess = null, Key = null;
            Console.WriteLine("Please enter the encrypted message:" + "\n");
            EncryptedMess = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Please enter his key:" + "\n");
            Key = Console.ReadLine();
            Console.WriteLine();
            char[] Mess = EncryptedMess.ToCharArray();
            char[] KeyArr = Key.ToCharArray();
            char[] KeyLengthMess = new char[Mess.Length];
            for (int i = 0; i < Mess.Length; i++)
            {
                KeyLengthMess[l] = KeyArr[k];
                k++;
                l++;
                if (k == KeyArr.Length)
                {
                    k = 0;
                }
            }
            return (KeyLengthMess, Mess);
        }
    }
}
