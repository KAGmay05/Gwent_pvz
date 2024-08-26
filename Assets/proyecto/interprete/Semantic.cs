public class SemanticNew
{
 Dictionary<string, Variable> variables = new Dictionary<string, Variable>();
 Dictionary<string, Effect> effects = new Dictionary<string, Effect>();
 Dictionary<string, Card> cards = new Dictionary<string, Card>();
 Stack<Dictionary<string, Variable>> scopes = new Stack<Dictionary<string, Variable>>();
 public void CheckNode(Node node)
 
    {
        Console.WriteLine("esta en checking");
        if(node is Program program)
        {
             foreach (var effect in program.Effects)
             {
                 Console.WriteLine(" node is an effect");
                 string effectname = ((String)effect.Name.name).Value;
                 if(effects.ContainsKey(effectname))
                 {
                    throw new SemanticError("Effect already defined");
                 }
                 else effects[effectname] = effect;
                 Variable effectVar = new Variable(new Token(TokenType.IDENTIFIER, effectname, effectname, 0, 0));
                 effectVar.type = Variable.Type.EFFECT;
                 if(variables.ContainsKey(effectname))
                 {
                   throw new SemanticError($"A local variable or function named {effectname} is already defined");
                 }
                 else variables[effectname] = effectVar;
                 CheckEffect(effect);

             }
             foreach (var card in program.Cards)
             {
                 Console.WriteLine(" node is a card");
                 string cardname = ((String)card.Name.name).Value;
                 if(cards.ContainsKey(cardname))
                 {
                    throw new SemanticError("Card already defined");
                 }
                 else cards[cardname] = card;
                 Variable cardVar = new Variable(new Token(TokenType.IDENTIFIER, cardname, cardname, 0, 0));
                 cardVar.type = Variable.Type.CARD;
                 if(variables.ContainsKey(cardname))
                 {
                   throw new SemanticError($"A local variable named {cardname} is already defined");
                 }
                 else variables[cardname] = cardVar;
                 CheckCard(card);
                
             }
        }
    }
    public void CheckCard(Card card)
    {
     PushScope();
     if(card.Range == null)
     {
        throw new SemanticError("Card must have a range");
     }
     CheckExpression(card.Type.type);
     CheckExpression(card.Name.name);
     CheckExpression(card.Faction.faction);
     CheckExpression(card.Power.power);
     if(card.Range.range == null)
     {
        foreach (var expr in card.Range.range)
        {
            CheckExpression(expr);
        }
     }
     if(card.OnActivation != null)
     {
        foreach(var element in card.OnActivation.Elements)
        {
            CheckOnActivationE(element);
        }
     }
     PopScope();
    }
    void CheckOnActivationE(OnActivationElements element)
    {
        PushScope();
        if(element.OAEffect !=null) CheckOAEffect(element.OAEffect);
        if(element.Selector != null) CheckSelector(element.Selector);
        if(element.postAction != null) CheckPostAction(element.postAction);
        PopScope();
    }
    void CheckOAEffect(OAEffect oaEffect)
    {
     PushScope();
     if(!effects.ContainsKey(oaEffect.Name) )
     {
      throw new SemanticError("Effect not defined");
     }
     PopScope();
    }
    void CheckSelector(Selector selector)
    {

    }
    void CheckPostAction(PostAction postAction)
    {

    }
    public void CheckEffect(Effect effect)
    {
        PushScope();
        CheckName(effect.Name.name);
        if (effect.Params != null)
        {
            foreach (var param in effect.Params.Arguments)
            {
                if (param is Variable variable)
                {
                    DefineVariable(variable);
                }
            }
        }

        CheckAction(effect.Action);

        PopScope();
    }
    void CheckName(Expression name)
    {
    if(name == null || name is String stringName && string.IsNullOrWhiteSpace(stringName.Value)  )
    {
     throw new SemanticError("Name cannot be null or empty");
    }
    }
 void PushScope()
 {
    scopes.Push(new Dictionary<string, Variable>());
 }
 void PopScope()
 {
    scopes.Pop();
 }
 public  void DefineVariable(Variable variable)
 {
    if(scopes.Count == 0)
    {
        PushScope();
    }
    scopes.Peek()[variable.Value] = variable;
    variables[variable.Value] = variable;
 }
 public bool IsVariableDeclared(string name)
 {
    foreach(var scope in scopes)
    {
        if(scope.ContainsKey(name)) return true;
    }
    return false;
 }
 public void CheckExpression(Expression expr)
 {
    if(expr is Variable variable)
    {
        if(!IsVariableDeclared(variable.Value))
        {
            // throw new SemanticError($"Variable '{variable.Value}' is not declared.");
        }
    }
    else if (expr is Binary binary)
    {
        CheckExpression(binary.Left);
        CheckExpression(binary.Right);
    }
    else if(expr is Unary unary)
    {
        CheckExpression(unary.Right);
    }
 }
 public void CheckAction(Action action)
 {
    PushScope();
    DefineVariable(new Variable(new Token(TokenType.IDENTIFIER, "targets", "targets", 0, 0)));
    DefineVariable(new Variable(new Token(TokenType.IDENTIFIER, "context", "context", 0, 0)));
    foreach(var statement in action.Block.statements)
    {
        CheckStatement(statement);
    }
    PopScope();
 }
 public void CheckStatement(Statement statement)
 {
    if(statement is ForStatement forStatement)
    {
        PushScope();
        DefineVariable(forStatement.Target);
        CheckVariable(forStatement.Targets);
        CheckStatementBlock(forStatement.Body);
        PopScope();
    }
    else if(statement is Assignment assignment)
    {
        CheckAssignement(assignment);
    }
    else if(statement is WhileStatement whileStatement)
    {
        CheckExpression(whileStatement.Condition);
        CheckStatementBlock(whileStatement.Body);
    }
 }
 public void CheckVariable(Variable variable)
 {
    if(!IsVariableDeclared(variable.Value))
    {
        // throw new SemanticError($"Variable '{variable.Value}' is not declared.");
    }
    if(variable is VariableComp variableComp)
    {
        Console.WriteLine("esta en variablcomp");
        // foreach(var node in variableComp.args.Arguments)
        // {
        //     Console.WriteLine("foreach");
        //     if(node is FunctionDeclaration function)
        //      {
        //         Console.WriteLine("function");
        //         CheckFunction(function);
        //      }
        //     else if (node is Expression expression)
        //     {
        //         CheckExpression(expression);
        //     }
        //     else if(node is Variable subvariable)
        //     {
        //      CheckVariable(variable);
        //     }
        // }
        for(int i = 0; i<variableComp.args.Arguments.Count(); i++)
        {
            var arg = variableComp.args.Arguments[i];
            foreach(var node in variableComp.args.Arguments)
            Console.WriteLine($"en la lista esta{node} y solo eso ");
            if(variableComp.args.Arguments[i] is FunctionDeclaration function)
            {
                Console.WriteLine("function");
                CheckFunction(function);
            }
            else 
            Console.WriteLine("no entro");
        }
    }
 }
 public void CheckStatementBlock(StatementBlock statementBlock)
 {
    PushScope();
    foreach(var statement in statementBlock.statements)
    {
        CheckStatement(statement);
    }
    PopScope();
 }
 public void CheckAssignement(Assignment assignment)
 {
    if(assignment.Left is VariableComp variableComp)
    {
        CheckVariable(variableComp);
    }
    if(!IsVariableDeclared(assignment.Left.Value))
    {
        // throw new SemanticError($"Variable '{assignment.Left.Value}' is not declared.");
    }
    CheckExpression(assignment.Right);
 }
 void CheckFunction(FunctionDeclaration function)
 {
    Variable functionvar = new Variable(new Token(TokenType.FUN, function.FunctionName, function.FunctionName, 0,0));
    variables[function.FunctionName] = functionvar;
    foreach(var param in function.Args.Arguments)
    {
        if(param is Expression expression)
        {
            CheckExpression(expression);
        }
        else if(param is Variable variable)
        {
            CheckVariable(variable);
        }
    }
    Console.WriteLine(function.FunctionName);
    if(function.FunctionName == "Pop"|| function.FunctionName == "Shuffle")
    {
     Console.WriteLine("entro a function pop");
     if(function.Args.Arguments != null && function.Args.Arguments.Any())
     {
        throw new SemanticError("Invalid params for the function declared");
     }
    }
    // else if(function.FunctionName == "Push" || function.FunctionName == "SendBottom")
    // {
    //     if(function.Args.GetType() != Card)

    // }
 }
}
public class SemanticError : Exception
{
    public SemanticError(string message) : base(message) { }
}
