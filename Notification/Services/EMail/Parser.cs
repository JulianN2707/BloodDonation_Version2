using System.Collections;

namespace MVCT.Sisfv.Tansversales.Notificaciones.Worker.Services.EMail;
public class Parser
{
    private string _strTemplateBlock;
    private Hashtable _hstValues;
    private string _parsedBlock;

    private readonly Dictionary<string, Parser> _blocks = new();

    private readonly string _variableTagBegin = "##";
    private readonly string _variableTagEnd = "##";

    private readonly string _modificatorTag = ":";
    private readonly string _modificatorParamSep = ",";

    private readonly string _conditionTagIfBegin = "##If--";
    private readonly string _conditionTagIfEnd = "##";
    private readonly string _conditionTagElseBegin = "##Else--";
    private readonly string _conditionTagElseEnd = "##";
    private readonly string _conditionTagEndIfBegin = "##EndIf--";
    private readonly string _conditionTagEndIfEnd = "##";

    private readonly string _blockTagBeginBegin = "##BlockBegin--";
    private readonly string _blockTagBeginEnd = "##";
    private readonly string _blockTagEndBegin = "##BlockEnd--";
    private readonly string _blockTagEndEnd = "##";

    public string TemplateBlock
    {
        get => _strTemplateBlock;
        set
        {
            _strTemplateBlock = value;
            ParseBlocks();
        }
    }

    public Hashtable Variables
    {
        set => _hstValues = value;
    }

    public Hashtable ErrorMessage { get; } = new();

    public Dictionary<string, Parser> Blocks
    {
        get { return _blocks; }
    }

    #region Contructors

    private Parser()
    {
        _strTemplateBlock = "";
    }

    public Parser(string filePath)
    {
        ReadTemplateFromFile(filePath);
        ParseBlocks();
    }

    public Parser(Hashtable variables)
    {
        _hstValues = variables;
    }

    public Parser(string filePath, Hashtable variables)
    {
        ReadTemplateFromFile(filePath);
        _hstValues = variables;
        ParseBlocks();
    }

    #endregion Contructors

    public void SetTemplateFromFile(string filePath)
    {
        ReadTemplateFromFile(filePath);
    }

    public void SetTemplate(string templateBlock)
    {
        TemplateBlock = templateBlock;
    }

    public string Parse()
    {
        ParseConditions();
        ParseVariables();
        return _parsedBlock;
    }

    public string ParseBlock(string blockName, Hashtable variables)
    {
        if (!_blocks.ContainsKey(blockName))
        {
            throw new ArgumentException(string.Format("Could not find Block with Name '{0}'", blockName));
        }

        _blocks[blockName].Variables = variables;
        return _blocks[blockName].Parse();
    }

    public bool ParseToFile(string filePath, bool replaceIfExists)
    {
        if (File.Exists(filePath) && !replaceIfExists)
        {
            return false;
        }

        StreamWriter sr = File.CreateText(filePath);
        sr.Write(Parse());
        sr.Close();
        return true;
    }

    private void ReadTemplateFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new ArgumentException("Template file does not exist.");
        }

        StreamReader reader = new StreamReader(filePath);
        TemplateBlock = reader.ReadToEnd();
        reader.Close();
    }

    private void ParseBlocks()
    {
        //int idxPrevious = 0;
        int idxCurrent = 0;

        while ((idxCurrent = _strTemplateBlock.IndexOf(_blockTagBeginBegin, idxCurrent)) != -1)
        {
            string blockName;
            int idxBlockBeginBegin, idxBlockBeginEnd, idxBlockEndBegin;

            idxBlockBeginBegin = idxCurrent;
            idxCurrent += _blockTagBeginBegin.Length;

            // Searching for BlockBeginEnd Index

            idxBlockBeginEnd = _strTemplateBlock.IndexOf(_blockTagBeginEnd, idxCurrent);
            if (idxBlockBeginEnd == -1)
            {
                throw new Exception("Could not find BlockTagBeginEnd");
            }

            // Getting Block Name

            blockName = _strTemplateBlock.Substring(idxCurrent, idxBlockBeginEnd - idxCurrent);
            idxCurrent = idxBlockBeginEnd + _blockTagBeginEnd.Length;

            // Getting End of Block index

            string endBlockStatment = _blockTagEndBegin + blockName + _blockTagEndEnd;
            idxBlockEndBegin = _strTemplateBlock.IndexOf(endBlockStatment, idxCurrent);
            if (idxBlockEndBegin == -1)
            {
                throw new Exception("Could not find End of Block with name '" + blockName + "'");
            }

            // Add Block to Dictionary

            Parser block = new Parser();
            block.TemplateBlock = _strTemplateBlock.Substring(idxCurrent, idxBlockEndBegin - idxCurrent);
            _blocks.Add(blockName, block);

            // Remove Block Declaration From Template

            _strTemplateBlock = _strTemplateBlock.Remove(idxBlockBeginBegin, idxBlockEndBegin - idxBlockBeginBegin);

            idxCurrent = idxBlockBeginBegin;
        }
    }

    private void ParseConditions()
    {
        int idxPrevious = 0;
        int idxCurrent = 0;
        _parsedBlock = "";

        while ((idxCurrent = _strTemplateBlock.IndexOf(_conditionTagIfBegin, idxCurrent)) != -1)
        {
            string varName;
            string trueBlock, falseBlock;
            string elseStatment, endIfStatment;
            int idxIfBegin, idxIfEnd, idxElseBegin, idxEndIfBegin;
            bool boolValue;

            idxIfBegin = idxCurrent;
            idxCurrent += _conditionTagIfBegin.Length;

            // Searching for EndIf Index

            idxIfEnd = _strTemplateBlock.IndexOf(_conditionTagIfEnd, idxCurrent);
            if (idxIfEnd == -1)
            {
                throw new Exception("Could not find ConditionTagIfEnd");
            }

            // Getting Value Name

            varName = _strTemplateBlock.Substring(idxCurrent, idxIfEnd - idxCurrent);

            idxCurrent = idxIfEnd + _conditionTagIfEnd.Length;

            // Compare ElseIf and EndIf Indexes

            elseStatment = _conditionTagElseBegin + varName + _conditionTagElseEnd;
            endIfStatment = _conditionTagEndIfBegin + varName + _conditionTagEndIfEnd;
            idxElseBegin = _strTemplateBlock.IndexOf(elseStatment, idxCurrent);
            idxEndIfBegin = _strTemplateBlock.IndexOf(endIfStatment, idxCurrent);
            if (idxElseBegin > idxEndIfBegin)
            {
                throw new Exception("Condition Else Tag placed after Condition Tag EndIf for '" + varName + "'");
            }

            // Getting True and False Condition Blocks

            if (idxElseBegin != -1)
            {
                trueBlock = _strTemplateBlock.Substring(idxCurrent, idxElseBegin - idxCurrent);
                falseBlock = _strTemplateBlock.Substring(idxElseBegin + elseStatment.Length, idxEndIfBegin - idxElseBegin - elseStatment.Length);
            }
            else
            {
                trueBlock = _strTemplateBlock.Substring(idxCurrent, idxEndIfBegin - idxCurrent);
                falseBlock = "";
            }

            // Parse Condition

            try
            {
                boolValue = Convert.ToBoolean(_hstValues[varName]);
            }
            catch
            {
                boolValue = false;
            }

            string beforeBlock = _strTemplateBlock.Substring(idxPrevious, idxIfBegin - idxPrevious);

            if (_hstValues.ContainsKey(varName) && boolValue)
            {
                _parsedBlock += beforeBlock + trueBlock.Trim();
            }
            else
            {
                _parsedBlock += beforeBlock + falseBlock.Trim();
            }

            idxCurrent = idxEndIfBegin + endIfStatment.Length;
            idxPrevious = idxCurrent;
        }
        _parsedBlock += _strTemplateBlock.Substring(idxPrevious);
    }

    private void ParseVariables()
    {
        int idxCurrent = 0;

        while ((idxCurrent = _parsedBlock.IndexOf(_variableTagBegin, idxCurrent)) != -1)
        {
            string varName, varValue;
            int idxVarTagEnd;

            idxVarTagEnd = _parsedBlock.IndexOf(_variableTagEnd, idxCurrent + _variableTagBegin.Length);
            if (idxVarTagEnd == -1)
            {
                throw new Exception(string.Format("Index {0}: could not find Variable End Tag", idxCurrent));
            }

            // Getting Variable Name

            varName = _parsedBlock.Substring(idxCurrent + _variableTagBegin.Length, idxVarTagEnd - idxCurrent - _variableTagBegin.Length);

            // Checking for Modificators

            string[] varParts = varName.Split(_modificatorTag.ToCharArray());
            varName = varParts[0];

            // Getting Variable Value
            // If Variable doesn't exist in _hstValue then
            // Variable Value equal empty string

            // [added 6/6/2006] If variable is null than it will also has empty string

            varValue = string.Empty;

            if (_hstValues.ContainsKey(varName) && _hstValues[varName] != null)
            {
                varValue = _hstValues[varName].ToString();
            }

            // Apply All Modificators to Variable Value

            for (int i = 1; i < varParts.Length; i++)
            {
                ApplyModificator(ref varValue, varParts[i]);
            }

            // Replace Variable in Template

            _parsedBlock = _parsedBlock.Substring(0, idxCurrent) + varValue + _parsedBlock.Substring(idxVarTagEnd + _variableTagEnd.Length);

            // Add Length of added value to Current index
            // to prevent looking for variables in the added value
            // Fixed Date: April 5, 2006
            idxCurrent += varValue.Length;
        }
    }

    private void ApplyModificator(ref string value, string modificator)
    {
        // Checking for parameters

        string strModificatorName = "";
        string strParameters = "";
        int idxStartBrackets, idxEndBrackets;

        if ((idxStartBrackets = modificator.IndexOf("(")) != -1)
        {
            idxEndBrackets = modificator.IndexOf(")", idxStartBrackets);

            if (idxEndBrackets == -1)
            {
                throw new Exception("Incorrect modificator expression");
            }

            strModificatorName = modificator.Substring(0, idxStartBrackets).ToUpper();
            strParameters = modificator.Substring(idxStartBrackets + 1, idxEndBrackets - idxStartBrackets - 1);
        }
        else
        {
            strModificatorName = modificator.ToUpper();
        }

        string[] arrParameters = strParameters.Split(_modificatorParamSep.ToCharArray());

        for (int i = 0; i < arrParameters.Length; i++)
        {
            arrParameters[i] = arrParameters[i].Trim();
        }

        try
        {
            Type typeModificator = Type.GetType("TemplateParser.Modificators." + strModificatorName);

            if (typeModificator.IsSubclassOf(Type.GetType("TemplateParser.Modificators.Modificator")))
            {
                Modificator objModificator = (Modificator)Activator.CreateInstance(typeModificator);
                objModificator.Apply(ref value, arrParameters);
            }
        }
        catch
        {
            throw new Exception(string.Format("Could not find modificator '{0}'", strModificatorName));
        }
    }
}
