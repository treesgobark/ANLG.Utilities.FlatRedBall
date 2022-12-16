namespace ANLG.Utilities.StaticUtilities;

///
public static class StringUtilities
{
    /// <remarks>Uses <see cref="Char.IsWhiteSpace(Char)"/> to differentiate between words.</remarks>
    public static int FindLengthOfLargestWord(string text)
    {
        int biggestWordLength = 0;
        int currentWordLength = 0;

        for (int i = 0; i < text.Length; i++)
        {
            var currentCharacter = text[i];
            if (char.IsWhiteSpace(currentCharacter))
            {
                currentWordLength = 0;
            }
            else
            {
                currentWordLength++;
            }

            if (currentWordLength > biggestWordLength)
            {
                biggestWordLength = currentWordLength;
            }
        }

        return biggestWordLength;
    }

    /// <summary>
    /// Returns which characters in a given string should become line breaks given the maximum number of characters for each line.
    /// </summary>
    public static bool[] FindLineBreaks(string text, int maxCharactersPerLine)
    {
        bool[] lineBreaks = new bool[text.Length];

        int charsOnCurrentLine = 0;
        int? lastLineBreakIndex = null;
        for (int i = 0; i < text.Length; i++)
        {
            var currentCharacter = text[i];
            charsOnCurrentLine++;

            if (currentCharacter == '\n')
            {
                lineBreaks[i] = true;
                charsOnCurrentLine = 0;
                lastLineBreakIndex = i;
                continue;
            }
            else if (charsOnCurrentLine > maxCharactersPerLine)
            {
                if (lastLineBreakIndex is not null)
                {
                    lineBreaks[lastLineBreakIndex.Value] = true;
                    charsOnCurrentLine = 0;
                    continue;
                }
            }

            if (char.IsWhiteSpace(currentCharacter))
            {
                lastLineBreakIndex = i;
            }
        }

        return lineBreaks;
    }

    /// <summary>
    /// Returns the zero-indexed column and row for each character of a string with the given length and line breaks.
    /// <br/>See <see cref="FindLineBreaks"/>
    /// </summary>
    public static (int column, int row)[] FindCharacterTextBoxPositions(int textLength, bool[] lineBreaks)
    {
        (int, int)[] positions = new (int, int)[textLength];

        int xCounter = 0;
        int yCounter = 0;
        for (int i = 0; i < textLength; i++)
        {
            positions[i] = (xCounter, yCounter);
            if (lineBreaks[i])
            {
                xCounter = 0;
                yCounter++;
            }
            else
            {
                xCounter++;
            }
        }

        return positions;
    }
}
