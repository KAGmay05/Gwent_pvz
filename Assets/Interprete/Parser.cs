using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Parser : MonoBehaviour
{
     List<Token> Tokens{get;}
    List<string> Errors = new List<string>();
     int current = 0;
    public Parser(List<Token> tokens, List<string>errors)
    {
        Tokens = tokens;
        Errors= errors;
    }
    public Node Parse()
    {
        Program Program = new Program();
        while(!IsAtEnd())
        {
            if(Match(TokenType.CARD))
            {
                Consume(TokenType.LEFT_BRACE,"Expected '{' after card");
                Program.CardIs.Add(ParseCard());
                Consume(TokenType.RIGHT_BRACE,"Expected '}' after card declaration");
            }
            else if(Match(TokenType.EFFECT))
            {
                Consume(TokenType.LEFT_BRACE,"Expected '{'");
                Program.Effects.Add(ParseEffect());
                Consume(TokenType.RIGHT_BRACE,"Expected '}' after effect declaration");
            }
            else
            {
                Errors.Add($"'{Peek().Lexeme}' in {Peek().Line}: Card or Effect expected.");
            }
        }
        return Program;
    }

    CardI ParseCard()
    {
        CardI card = new CardI();
        int[] counter = new int[6];
        while(!Check(TokenType.RIGHT_BRACE) && !IsAtEnd())
        {
            if(Match(TokenType.TYPE))
            {
                counter[0]+=1;
                Consume(TokenType.COLON,"Expected ':' after Type");
                card.Type = new Type(ParseExpression());
                Consume(TokenType.COMMA,"Expected ',' after expression");
            }
            else if(Match(TokenType.NAME))
            {
                counter[1]+=1;
                Consume(TokenType.COLON,"Expected ':' after Name");
                card.Name = new Name(ParseExpression());
                Consume(TokenType.COMMA,"Expected ',' after expression");
            }
            else if(Match(TokenType.FACTION))
            {
                counter[2]+=1;
                Consume(TokenType.COLON,"Expected ':' after Faction");
                card.Faction = new Faction(ParseExpression());
                Consume(TokenType.COMMA,"Expected ',' after expression");
            }
            else if(Match(TokenType.POWER))
            {
                counter[3]+=1;
                Consume(TokenType.COLON,"Expected ':' after Power");
                card.Power = new Power(ParseExpression());
                Consume(TokenType.COMMA,"Expected ',' after expression");
            }
            else if(Match(TokenType.RANGE))
            {
                counter[4]+=1;
                Consume(TokenType.COLON,"Expected ':' after Range");
                Consume(TokenType.LEFT_BRACK,"Expected '['");
                List<Expression> expressions = new List<Expression>();
                for(int i=0;i<3;i++)
                {
                    expressions.Add(ParseExpression());
                    if(Match(TokenType.COMMA)) continue;
                    else break;
                }
                Consume(TokenType.RIGHT_BRACK,"Expected ']'");
                Consume(TokenType.COMMA,"Expected ',' after Range");
                card.Range = new Range(expressions.ToArray());
            }
            else if(Match(TokenType.ONACTIVATION))
            {
                counter[5]+=1;
                card.OnActivation = ParseOnActivation();
            }
            else
            {
                Errors.Add($"'{Peek().Lexeme}' in {Peek().Line}: Invalid Card property.");
            }
        }
        if(counter[0]<1) Errors.Add("A Type property is missing from card");
        else if(counter[0]>1) Errors.Add("Only one Type is allowed");
        if(counter[1]<1) Errors.Add("A Name property is missing from card");
        else if(counter[1]>1) Errors.Add("Only one Name is allowed");
        if(counter[2]<1) Errors.Add("A Faction property is missing from card");
        else if(counter[2]>1) Errors.Add("Only one Faction is allowed");
        if(counter[3]<1) Errors.Add("A Power property is missing from card");
        else if(counter[3]>1) Errors.Add("Only one Power is allowed");
        if(counter[4]<1) Errors.Add("A Range property is missing from card");
        else if(counter[4]>1) Errors.Add("Only one Range is allowed");
        if(counter[5]<1) Errors.Add("An OnActivation property is missing from card");
        else if(counter[5]>1) Errors.Add("Only one OnActivation is allowed");
        return card;
    }

    Effect ParseEffect()
    {
        Effect effect = new Effect();
        int[] counter = new int[3];
        while(!Check(TokenType.RIGHT_BRACE) && !IsAtEnd())
        {
            if(Match(TokenType.NAME))
            {
                counter[0]+=1;
                Consume(TokenType.COLON,"Expected ':' after Name");
                effect.Name = new Name(ParseExpression());
                Consume(TokenType.COMMA,"Expected ',' after expression");
            }
            else if(Match(TokenType.PARAMS))
            {
                counter[1]+=1;
                Consume(TokenType.COLON,"Expected ':' after Params");
                effect.Params = GetParams();
            }
            else if(Match(TokenType.ACTION))
            {
                counter[2]+=1;
                Consume(TokenType.COLON,"Expected ':' after Action");
                effect.Action = ParseAction();
            }
            else
            {
                Errors.Add($"'{Peek().Lexeme}' in {Peek().Line}: Invalid Effect property.");
            }
        }
        if(counter[0]<1) Errors.Add("A Name property is missing from effect");
        else if(counter[0]>1) Errors.Add("Only one Name is allowed");
        if(counter[1]>1) Errors.Add("Only one Params is allowed");
        if(counter[2]<1) Errors.Add("An Action property is missing from effect");
        else if(counter[2]>1) Errors.Add("Only one Action is allowed");
        return effect;
    }

    OnActivation ParseOnActivation()
    {
        Consume(TokenType.COLON,"Expected ':' after OnActivation");
        Consume(TokenType.LEFT_BRACK,"Expected '['");
        OnActivation onActivation = new OnActivation();
        while(!Check(TokenType.RIGHT_BRACK) && !IsAtEnd())
        {
            onActivation.Elements.Add(ParseOAE());
        }
        Consume(TokenType.RIGHT_BRACK,"Expected ']'");
        return onActivation;
    }

    OnActivationElements ParseOAE()
    {
        Consume(TokenType.LEFT_BRACE,"Expected '{'");
        OAEffect onActivationEffect = null!;
        Selector selector = null!;
        PostAction postAction = null!;
        while(!Check(TokenType.RIGHT_BRACE) && !IsAtEnd())
        {
            if(Match(TokenType.ONACTIVATIONEFFECT))
            {
                if(onActivationEffect==null)
                {
                    Consume(TokenType.COLON,"Expected ':'");
                    onActivationEffect = ParseOAEffect();
                }
                else
                {

                }
            }
            else if(Match(TokenType.SELECTOR))
            {
                if(selector == null)
                {
                    Consume(TokenType.COLON,"Expected ':'");
                    selector = ParseSelector();
                    if(selector.Source == null) Errors.Add($"'{Peek().Lexeme}' in {Peek().Line}: Missing field");
                    //Consume(TokenType.COMMA,"Expected ','");
                }
                else
                {

                }
            }
            else if(Match(TokenType.POSTACTION))
            {
                if(postAction == null)
                {
                    Consume(TokenType.COLON,"Expected ':'");
                    postAction = ParsePostAction();
                }
                else
                {

                }
            }
            else
            {
                Errors.Add($"'{Peek().Lexeme}' in {Peek().Line}: Invalid OnActivation field.");
            }
        }
        Consume(TokenType.RIGHT_BRACE,"Expected '}' after OnActivation declaration");
        return new OnActivationElements(onActivationEffect,selector,postAction);
    }

    OAEffect ParseOAEffect()
    {
        string name = null!;
        List<Assignment> assignments = new List<Assignment>();
        if(Check(TokenType.STRING))
        {
            name = Advance().Lexeme.Substring(1,Previous().Lexeme.Length-2);
            Consume(TokenType.COMMA,"Expected ','");
            while(!Check(TokenType.SELECTOR))
            {
                if(Check(TokenType.IDENTIFIER))
                {
                    Variable variable = ParseVariable();
                    Token token = Peek();
                    Consume(TokenType.COLON,"Expected ':'");
                    Expression expression = ParseExpression();
                    Assignment assignment = new Assignment(variable,token,expression);
                    assignments.Add(assignment);
                    Consume(TokenType.COMMA,"Expected ','");
                }   
                else
                {

                }
            }
        }
        else
        {
            Consume(TokenType.LEFT_BRACE,"Expected '{'");
            while(!Check(TokenType.RIGHT_BRACE) && !IsAtEnd())
            {
                if(Match(TokenType.NAME))
                {
                    if(Match(TokenType.COLON))
                    {
                        if(name == null)
                        {
                            name = Advance().Lexeme.Substring(1,Previous().Lexeme.Length-2);
                            if(!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA,"Expected ','");
                        }
                        else
                        {
                            Errors.Add($"'{Peek().Lexeme}' in {Peek().Line}: Duplicate");
                        }
                    }
                    else
                    {
                        if(name == null)
                        {
                            name = Advance().Lexeme.Substring(1,Previous().Lexeme.Length-2);
                            if(!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA,"Expected ','");
                        }
                        else
                        {
                            Errors.Add($"'{Peek().Lexeme}' in {Peek().Line}: Duplicate");
                        }
                    }
                }
                else if(Check(TokenType.IDENTIFIER))
                {
                    Variable variable = ParseVariable();
                    Token token = Peek();
                    Consume(TokenType.COLON,"Expected ':'");
                    Expression expression = ParseExpression();
                    Assignment assignment = new Assignment(variable,token,expression);
                    assignments.Add(assignment);
                    if(!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA,"Expected ','");
                }
                else
                {

                }
            }
            Consume(TokenType.RIGHT_BRACE,"Expected '}'");
        }
        if(name == null) Errors.Add($"'{Peek().Lexeme}' in {Peek().Line}: No name");
        return new OAEffect(name,assignments);
    }

    Selector ParseSelector()
    {
        Consume(TokenType.LEFT_BRACE,"Expected '{'");
        string source = null!;
        Single single = null!;
        Predicate predicate = null!;
        while(!Check(TokenType.RIGHT_BRACE) && !IsAtEnd())
        {
            if(Match(TokenType.SOURCE))
            {
                Consume(TokenType.COLON,"Expected ':'");
                if(source == null)
                {
                    if(System.Convert.ToString(Peek().Literal) == "deck")
                    {
                        source = Advance().Lexeme;
                        source = "deck";
                    }
                    else if(System.Convert.ToString(Peek().Literal) == "board")
                    {
                         Debug.Log("en el if del delectr");
                         source = Advance().Lexeme;
                         source = "board";
                         Debug.Log($"{source} es lo q hay en sorce");
                    }
                    else if (System.Convert.ToString(Peek().Literal) == "otherDeck")
                    {
                        source = Advance().Lexeme;
                        source = "otherDeck";
                    }
                    else if(System.Convert.ToString(Peek().Literal) == "hand")
                    {
                        source = Advance().Lexeme;
                        source = "hand";
                    }
                    else if (System.Convert.ToString(Peek().Literal) == "otherHand")
                    {
                        source = Advance().Lexeme;
                        source = "otherHand";
                    }
                    else if(System.Convert.ToString(Peek().Literal) == "field")
                    {
                        source = Advance().Lexeme;
                        source = "field";
                    }
                    else if(System.Convert.ToString(Peek().Literal) == "otherField")
                    {
                        source = Advance().Lexeme;
                        source = "otherfield";
                    }
                    else if(System.Convert.ToString(Peek().Literal) == "parent")
                    {
                        source = Advance().Lexeme;
                        source = "parent";
                    }
                    // if(System.Convert.ToString(Peek().Literal) == "deck"||System.Convert.ToString(Peek().Literal) == "otherDeck"||System.Convert.ToString(Peek().Literal) == "hand"||System.Convert.ToString(Peek().Literal) == "otherHand"||System.Convert.ToString(Peek().Literal) == "field"||System.Convert.ToString(Peek().Literal) == "otherField"||System.Convert.ToString(Peek().Literal) == "parent"||System.Convert.ToString(Peek().Literal) == "board")
                    // {
                    //     Debug.Log("en el if del delectr");
                    // //    if(!IsAtEnd()) current++;
                    //     // Debug.Log($"{Advance().Lexeme} lo q hay en previous");
                    //     source = Advance().Lexeme;
                    //     source = "board";
                    //     Debug.Log($"{source} es lo q hay en sorce");
                    // }
                    else
                    {
                        Errors.Add("");
                    }
                }
                else
                {
                    Errors.Add($"'{Peek().Lexeme}' in {Peek().Line}: Duplicate");
                }
                if(!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA,"Expected ','");
            }
            else if(Match(TokenType.SINGLE))
            {
                Consume(TokenType.COLON,"Expected ':'");
                if(single == null)
                {
                    single = new Single(Advance());
                    Debug.Log($"{single.Value} este es single");
                }
                else
                {
                    Errors.Add($"'{Peek().Lexeme}' in {Peek().Line}: Duplicate");
                }
                if(!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA,"Expected ','");
            }
            else if(Match(TokenType.PREDICATE))
            {
                Consume(TokenType.COLON,"Expected ':'");
                if(predicate == null)
                {
                    predicate = ParsePredicate();
                }
                else
                {
                    Errors.Add($"'{Peek().Lexeme}' in {Peek().Line}: Duplicate");
                }
                if(!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA,"Expected ','");
            }
            else
            {
                Errors.Add($"'{Peek().Lexeme}' in {Peek().Line}: Invalid field");
            }
        }
        Consume(TokenType.RIGHT_BRACE,"Expected '}' after Selector declaration");
        if(single == null || predicate == null) Errors.Add($"'{Peek().Lexeme}' in {Peek().Line}: Missing field");
        return new Selector(source,single,predicate);
    }

    PostAction ParsePostAction()
    {
        Consume(TokenType.LEFT_BRACE,"Expected '{'");
        Expression expression = null!;
        Selector selector = null!;
        List<Assignment> assignments = new List<Assignment>();
        while(!Check(TokenType.RIGHT_BRACE) && !IsAtEnd())
        {
            if(Match(TokenType.TYPE))
            {
                Consume(TokenType.COLON,"Expected ':'");
                expression = ParseExpression();
                if(!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA,"Expected ','");
            }
            else if(Match(TokenType.SELECTOR))
            {
                Consume(TokenType.COLON,"Expected ':'");
                selector = ParseSelector();
                if(selector.Source == null)
                {
                    selector.Source = "parent";
                }
                if(!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA,"Expected ','");
            }
            else if(Check(TokenType.IDENTIFIER))
            {
                Variable variable = ParseVariable();
                Token token = Peek();
                Consume(TokenType.COLON,"Expected ':'");
                Expression exp = ParseExpression();
                Assignment assignment = new Assignment(variable,token,exp);
                assignments.Add(assignment);
                if(!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA,"Expected ','");
            }
            else
            {
                Errors.Add("");
            }
        }
        Consume(TokenType.RIGHT_BRACE,"Expected '}'");
        if(expression == null || selector == null) Errors.Add("");
        return new PostAction(expression,selector);
    }

    Predicate ParsePredicate()
    {
        Consume(TokenType.LEFT_PAREN,"Expected '('");
        Variable unit = ParseVariable();
        unit.type = Variable.Type.CARDI;
        Consume(TokenType.RIGHT_PAREN,"Expected ')'");
        Consume(TokenType.EQUAL_GREATER,"Expected '=>'");
        Expression expression = ParseExpression();
        return new Predicate(unit,expression);
    }

    Action ParseAction()
    {
        Consume(TokenType.LEFT_PAREN,"Expected '('");
        Variable target = ParseVariable();
        Consume(TokenType.COMMA,"Expected ','");
        Variable context = ParseVariable();
        Consume(TokenType.RIGHT_PAREN,"Expected ')'");
        Consume(TokenType.EQUAL_GREATER,"Expected '=>'");
        Consume(TokenType.LEFT_BRACE,"Expected '{'");
        StatementBlock StatementBlock = ParseStmsBlock();
        Consume(TokenType.RIGHT_BRACE,"Expected '}'");
        return new Action(target,context,StatementBlock);
    }

    Variable ParseVariable()
    {
        Variable variable = new Variable(Advance());
        if(Check(TokenType.DOT))
        {
            VariableComp variableComp = new VariableComp(variable.Token);
            Variable.Type varType = Variable.Type.NULL;
            while(Match(TokenType.DOT) && !IsAtEnd())
            {
                if(Match(TokenType.FUN))
                {
                    FunctionDeclaration function = ParseFunction(Previous().Lexeme);
                    varType = function.Type;
                    variableComp.args.Arguments.Add(function);
                    foreach(var arg in variableComp.args.Arguments)
                    Debug.Log($"En la lista esta {arg} y solo eso y function es {function}");
                }
                else
                {
                    if(Match(TokenType.TYPE))
                    {
                        Type type = new Type(new String(Previous().Lexeme));
                        varType = Variable.Type.STRING;
                        variableComp.args.Arguments.Add(type);
                    }
                    else if(Match(TokenType.NAME))
                    {
                        Name name = new Name(new String(Previous().Lexeme));
                        varType = Variable.Type.STRING;
                        variableComp.args.Arguments.Add(name);
                    }
                    else if(Match(TokenType.FACTION))
                    {
                        Faction faction = new Faction(new String(Previous().Lexeme));
                        varType = Variable.Type.STRING;
                        variableComp.args.Arguments.Add(faction);
                    }
                    else if(Match(TokenType.POWER))
                    {
                        PowerAsField power = new PowerAsField();
                        varType = Variable.Type.INT;
                        variableComp.args.Arguments.Add(power);
                    }
                    else if(Match(TokenType.RANGE))
                    {
                        Range range = new Range(Previous().Lexeme);
                        varType = Variable.Type.STRING;
                        variableComp.args.Arguments.Add(range);
                    }
                    else if(Match(TokenType.POINTER))
                    {
                        Pointer pointer = new Pointer(Previous().Lexeme);
                        //varType = Variable.Type.STRING;
                        variableComp.args.Arguments.Add(pointer);
                    }
                    else
                    {
                        //Error
                    }
                }
            }
            variable = variableComp;
            variable.type = varType;
        }
        return variable;
    }

    StatementBlock ParseStmsBlock()
    {
        StatementBlock block = new StatementBlock();
        while(!Check(TokenType.RIGHT_BRACE) && !IsAtEnd())
        {
            block.statements.Add(ParseStm());
        }
        return block;
    }

    Statement ParseStm()
    {
        if(Match(TokenType.FOR))
        {
           return ParseForStm();
        }
        else if(Match(TokenType.WHILE))
        {
            return ParseWhileStm();
        }
        else if(Check(TokenType.IDENTIFIER))
        {
            Variable variable = ParseVariable();
            Debug.Log(Peek().Lexeme);
            if(variable is VariableComp && Check(TokenType.SEMICOLON))
            {
                VariableComp v = (variable as VariableComp)!;
                if(v.args.Arguments[v.args.Arguments.Count-1].GetType() == typeof(FunctionDeclaration))
                {
                    FunctionDeclaration function = (v.args.Arguments[v.args.Arguments.Count-1] as FunctionDeclaration)!;
                    if(function.Type != Variable.Type.VOID) Errors.Add("");
                }
                else
                {
                    //Error
                }
                Consume(TokenType.SEMICOLON,"Expected ';'");
                return (variable as VariableComp)!;
            }
            else
            {
                return ParseAssignment(variable);
            }
        }
        else if(Check(TokenType.FUN))
        {
            return ParseFunction(Previous().Lexeme);
        }
        else
        {
            Errors.Add("");
            throw new Error(" ");
        }
    }

    ForStatement ParseForStm()
    {
        Variable target = ParseVariable();
        Consume(TokenType.IN,"Expected 'in'");
        Variable targets = ParseVariable();
        Consume(TokenType.LEFT_BRACE,"Expected {");
        StatementBlock stms = ParseStmsBlock();
        Consume(TokenType.RIGHT_BRACE,"Expected }");
        Consume(TokenType.SEMICOLON,"Expected ';'");
        return new ForStatement(target,targets,stms);
    }

    WhileStatement ParseWhileStm()
    {
        Consume(TokenType.LEFT_PAREN,"Expected '('");
        Expression expression = ParseExpression();
        Consume(TokenType.RIGHT_PAREN,"Expected ')'");
        StatementBlock stms = ParseStmsBlock();
        return new WhileStatement(expression,stms);
    }

    Assignment ParseAssignment(Variable variable)
    {
        Token op = Advance();
        Expression expression = ParseExpression();
        Consume(TokenType.SEMICOLON,"Expected ';'");
        return new Assignment(variable,op,expression);
    }

    FunctionDeclaration ParseFunction(string name)
    {
        Consume(TokenType.LEFT_PAREN,"Expected '('");
        Args args = new Args();
        while(!Check(TokenType.RIGHT_PAREN) && !IsAtEnd())
        {
            if(Check(TokenType.IDENTIFIER))
            {
                args.Arguments.Add(ParseVariable());
            }
            else if(Check(TokenType.FUN))
            {
                args.Arguments.Add(ParseFunction(Advance().Lexeme));
            }
            else
            {
                args.Arguments.Add(ParseExpression());
            }
            if(!Check(TokenType.RIGHT_PAREN)) Consume(TokenType.COMMA,"Expected ','");
        }
        Consume(TokenType.RIGHT_PAREN,"Expected ')'");
        FunctionDeclaration function = new FunctionDeclaration(name,args);
        return function;
    }

    Args GetParams()
    {
        Consume(TokenType.LEFT_BRACE,"Expected '{' after Params");
        Args variables = new Args();
        while(!Check(TokenType.RIGHT_BRACE) && !IsAtEnd())
        {
            var variable = ParseVariable();
            Consume(TokenType.COLON,"Expected ':' after parameter");
            if(Check(TokenType.STRINGTYPE)||Check(TokenType.NUMBERTYPE)||Check(TokenType.BOOLTYPE))
            {
                variable.TypeParam(Advance().Type);
                variables.Arguments.Add(variable);
                if(!Check(TokenType.RIGHT_BRACE))
                {
                    Consume(TokenType.COMMA,"Expected ','");
                }
            }
            else
            {
                throw new System.Exception("Expected type after parameter name");
                //return variables
            }
        }
        Consume(TokenType.RIGHT_BRACE,"Expected '}' after Params declaration");
        Consume(TokenType.COMMA,"Expected ','");
        return variables;
    }

    Expression ParseExpression()
    {
        try
        {
            var result = Equality();
            /*if (!IsAtEnd())
            {
                Errors.Add($"Unexpected token '{Peek().Lexeme}' at line {Peek().Line}");
            }*/
            return result;
        }
        catch(Error exception)
        {
            Debug.Log($"Semantical error:{exception.Message}");
            throw;
        }
    }

    Expression Equality()
    {
        Expression expression = Comparison();
        while(Match(TokenType.BANG_EQUAL)||Match(TokenType.EQUAL_EQUAL))
        {
            Token operators = Previous();
            Expression right = Comparison();
            expression = new BinaryBooleanExpression(expression,operators,right);
        }
        return expression;
    }

    Expression Comparison()
    {
        Expression expression = Term();
        while(Match(TokenType.GREATER)||Match(TokenType.GREATER_EQUAL)||Match(TokenType.LESS)||Match(TokenType.LESS_EQUAL))
        {
            Token operators = Previous();
            Expression right = Term();
            expression = new BinaryBooleanExpression(expression,operators,right);
        }
        return expression;
    }

    Expression Term()
    {
        Expression expression = Factor();
        if(Check(TokenType.PLUS) || Check(TokenType.MINUS))
        {
            while(Match(TokenType.PLUS)||Match(TokenType.MINUS))
            {
                Token operators = Previous();
                Expression right = Factor();
                expression = new BinaryIntergerExpression(expression,operators,right);
            }
        }
        else if(Check(TokenType.ATSIGN) || Check(TokenType.ATSIGN_ATSIGN))
        {
            while(Match(TokenType.ATSIGN)||Match(TokenType.ATSIGN_ATSIGN))
            {
                Token operators = Previous();
                Expression right = Factor();
                expression = new BinaryStringExpression(expression,operators,right);
            }   
        }
        return expression;
    }

    Expression Factor()
    {
        Expression expression = Unary();
        while(Match(TokenType.SLASH)||Match(TokenType.STAR)||Match(TokenType.PERCENT))
        {
            Token operators = Previous();
            Expression right = Unary();
            expression = new BinaryIntergerExpression(expression,operators,right);
        }
        return expression;
    }

    Expression Unary()
    {
        if(Match(TokenType.MINUS)||Match(TokenType.PLUS_PLUS))
        {
            Token operators = Previous();
            Expression right = Unary();
            return new UnaryIntergerExpression(operators,right);
        }
        else if(Match(TokenType.BANG))
        {
            Token operators = Previous();
            Expression right = Unary();
            return new UnaryBooleanExpression(operators,right);   
        }
        else if (Check(TokenType.IDENTIFIER) && LookAhead(TokenType.PLUS_PLUS))
        {
            Expression left = ParseVariable();
            Token operatorToken = Advance();
            return new UnaryIntergerExpression(operatorToken, left);
        }
        return Primary();
    }

    Expression Primary()
    {
        if(Match(TokenType.FALSE)) return new Bool(false);
        if(Match(TokenType.TRUE)) return new Bool(true);
        if(Match(TokenType.NUMBER)) return new Number(System.Convert.ToInt32(Previous().Literal));
        if(Match(TokenType.STRING))
        {
            return new String(Previous().Lexeme.Substring(1,Previous().Lexeme.Length-2));
        }
        if(Match(TokenType.LEFT_PAREN))
        {
            Expression expression = Equality();
            Consume(TokenType.RIGHT_PAREN,"Expect ')' after expression.");
            return new ExpressionGroup(expression);
        }
        if(Check(TokenType.IDENTIFIER))
        {
            return ParseVariable();
        }
        Errors.Add($"'{Peek().Lexeme}' in line {Peek().Line}: Unexpected token.");
        throw new Error(" ");
    }

    bool Match(TokenType type)
    {
        if(Check(type))
        {
            Advance();
            return true;
        }
        return false;
    }

    bool Check(TokenType type)
    {
        if(IsAtEnd()) return false;
        return Peek().Type == type;
    }
    
    bool LookAhead(TokenType type)
    {
        if (IsAtEnd()) return false;
        return Tokens[current+1].Type == type;
    }

    bool IsAtEnd()
    {
        return Peek().Type == TokenType.EOF;
    }

    Token Peek()
    {
        return Tokens[current];
    }

    Token Advance()
    {
        if(!IsAtEnd()) current++;
        return Previous();
    }

    Token Previous()
    {
        return Tokens[current-1];
    }

    Token Consume(TokenType type, string message)
    {
        Debug.Log(Peek().Type + " " + Peek().Lexeme);
        if(Check(type)) return Advance();
        Errors.Add($"'{Peek().Lexeme}' in line {Peek().Line}: {message}");
        throw new Error(" ");
    }
}
