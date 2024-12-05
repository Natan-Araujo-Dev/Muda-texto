using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static int extraTime;
    static int defaultTextingSpeed = 10;
    static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
    static async Task Main() //Tarefa: ajeitar sobre a palavra invertida com assento;
    {
        await delayedTextWrite("\n==================== Alterador de texto ====================\n", defaultTextingSpeed);

        await getComand();
    }

    static async Task getComand()
    {
        while (true)
        {
            await delayedTextWrite("\n------------------ Inicio ------------------\nDigite qual dessas opções você quer realizar:\n1: Inverter palavra;\n2: Mistificar palavra;\n3: Testar o novo método;\n4: Encerrar aplicação.", defaultTextingSpeed);
            string inputType = await requestInput("\n\nSua escolha: ", defaultTextingSpeed);
            string inputText;

            switch (inputType)
            {
                case "1":
                    inputText = await requestInput("\nDigite a palavra para inverter: ", defaultTextingSpeed);
                    if (inputText.Length >=2)
                    {
                        await delayedTextWrite("Texto invertido: " + InvertedWord(inputText) + "\n", defaultTextingSpeed);
                        await Task.Delay(3000 + extraTime);
                        extraTime = 0;
                    }
                    else
                    {
                        await delayedTextWrite("\nAlerta: Não é possível inverter essa Texto", defaultTextingSpeed);
                        await Task.Delay(2000);
                    }
                break;

                case "2":
                    inputText = await requestInput("\nTexto para mistificar: ", defaultTextingSpeed);
                    if (inputText.Length >=2)
                    {
                        await delayedTextWrite("Texto mistificado: " + strangeWord(inputText) + "\n", defaultTextingSpeed);
                        await Task.Delay(3000 + extraTime);
                        extraTime = 0;
                    }
                    else
                    {
                        await delayedTextWrite("\nAlerta: Não é possível mistificar essa Texto", defaultTextingSpeed);
                        await Task.Delay(2000);
                    }
                break;

                case "3":                
                    inputText = await requestInput("\nDigite alguma coisa: ", defaultTextingSpeed);
                    await delayedTextWrite(inputText + "\n", defaultTextingSpeed);
                    await Task.Delay(2000);
                break;

                case "4":
                    await delayedTextWrite("\nTem certeza? Escolha:\n1 = Sim\n2 = Não", defaultTextingSpeed);
                    string selectedOption = await requestInput("\n\nSua escolha: ", defaultTextingSpeed);
                    if (selectedOption == "1")
                    {   
                        await delayedTextWrite("\nSaindo..." + "\n", 125);
                        await Task.Delay(2000);
                        Environment.Exit(0);
                    }
                break;

                default: 
                    await delayedTextWrite("\nAlerta: Digite alguma das opções fornecidas\nRetornando...\n", defaultTextingSpeed);
                    await Task.Delay(1500);
                break;
            }
        }
    }

    static string InvertedWord (string wordToInvert)
    {
        char[] splitedWordToInvert = wordToInvert.ToCharArray();
        char[] splitedInvertedWord = new char[splitedWordToInvert.Length];

        int index = splitedWordToInvert.Length-1;
        foreach (char character in splitedWordToInvert)
        {   
            splitedInvertedWord[index] = character;
            index--;
        }

        string finalWord = new string (splitedInvertedWord);
        return finalWord;
    }

    static string strangeWord (string wordToChange)
    {
        //Isso é preguiça de criar um array com o alfabeto todo;
        char[] alfabet = new char[58]{
            '4', 'B', 'C', 'D', '3', 'F', 'G', 'H', '!', 'J', 'K', 'L', 'M',
            'N', '0', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            '[', '\\', ']', '^', '_', '´',
            '@', '6', 'c', 'd', '3', 'f', 'g', 'h', '!', 'j', 'k', '1', 'm',
            'n', '0', 'p', '9', 'r', 's', '+', 'u', 'v', 'w', 'x', 'y', 'z',
        };

        char[] splitedWordtoChange = wordToChange.ToCharArray();

        for (int i = 0; i < splitedWordtoChange.Length; i++)
        {
            //Pegar a posição da palavra, e mudar pelo seu respectivo ascii;
            int asciiValue = (int)splitedWordtoChange[i] - 65;
            if (asciiValue >= 0 && asciiValue <=58)
            {
                splitedWordtoChange[i] = alfabet[asciiValue];
            }
            else if (asciiValue == -65)
            {
                extraTime = 4000;
                Console.WriteLine("\nAlerta: caractere fora do padrão detectado. Evite usa-os\nO caractere anormal foi subistituido por '-'\n(erro do console VScode)");
                splitedWordtoChange[i] = '-';
            }
            else
            {
                splitedWordtoChange[i] = (char)(asciiValue + 65);
            }
        }
        string finalWord = new string (splitedWordtoChange);
        
        return finalWord;
    }

    static async Task delayedTextWrite (string baseText, int writeInterval)
    {
        char[] splitedText = baseText.ToCharArray();
  
        foreach (char character in splitedText)
        {
            Console.Write(character);
            await Task.Delay (writeInterval);
        }
    }

    static async Task<string> requestInput(string message, int writeInterval)
    {
        await delayedTextWrite(message, writeInterval);
        string input = await Task.Run(() => Console.ReadLine());
        return input;
    }
}