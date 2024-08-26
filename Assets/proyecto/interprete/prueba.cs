class Progra
{

    public static void Main(string[] args)
    {
         string h = @"effect{Name: ""Damage"",Params: {Amount: Number}, Action: (targets, context) => {for target in targets {i = 0;while (i++ < Amount)target.Power-=1;};}}card {Type: ""Oro"",Name: ""Geralt"",Faction: ""Neutral"",Power: 15,Range: [""Melee""],OnActivation: [{Effect: ""Damage"",Amount: 10,Selector: {Source: ""deck"",Single: true,Predicate: (unit) => unit.Faction == ""Monster""}}]}";
         string h2 = @"effect {Name: ""Boost"",Params: {amount: Number,duration: Number}, Action: (targets, context) =>{for target in targets {target.Power += amount;context.Board.Shuffle();};}}";
         string h3 = @"card {Type: ""Oro"",Name: ""Geralt"",Faction: ""Neutral"",Power: 15 ,Range: [""Melee""],OnActivation: [{Effect: ""Damage"",Amount: 10,Selector: {Source: ""deck"",Single: true,Predicate: (unit) => unit.Faction == ""Monster""}}]}";
         string h4 = @"effect{Name: ""Damage"",Params: {Amount: Number}, Action: (targets, context) => {for target in targets {i = 0;while (i++ < Amount)target.Power-=1;};}}effect {Name: ""Draw"",Action: (targets, context) => {topCard = context.Deck.Pop();context.Hand.Push(topCard);for target in targets{target.Power += 2;};}}card {Type: ""Oro"",Name: ""Triss"",Faction: ""Northern Realms"",Power: 12,Range: [""Ranged""],OnActivation: [{Effect: ""Draw"",Selector: {Source: ""deck"",Single: true,Predicate: (unit) => true}PostAction: {Type: ""Damage"",Amount: 5,Selector: {Source: ""deck"",Single: false,Predicate: (unit) => unit.Power < 5}}}]}";
         string h5 = @"effect {Name: ""Draw"",Action: (targets, context) => {topCard = context.Deck.Pop();context.Hand.Push(topCard);for target in targets{target.Power += 2;};}}";
         string h6 = @"effect {Name: ""Shield"",Params: {Amount: Number},Action: (targets, context) =>{for target in targets{target.Power += Amount;};}}card {Type: ""Plata"",Name: ""Shieldmaiden"",Faction: ""Skellige"",Power: 5,Range: [""Melee""],OnActivation: [{Effect: ""Shield"",Amount: 3,Selector: {Source: ""board"",Single: false,Predicate: (unit) => unit.Faction == ""Skellige""}}]}";
         string h7 = @"effect {Name: ""Boost"",Action: (targets, context) =>{for target in targets{target.Power += boostAmount;};}}";
         string h8 = @"card {Type: ""Oro"",Name: ""Ciri"",Faction: ""Neutral"",Power: 10,Range: [""Melee"", ""Ranged""],OnActivation: [{Effect: ""Teleport"",Amount: 2,Selector: {Source: ""board"",Single: true,Predicate: (unit) => unit.Faction == ""Nilfgaard""}}]}";
          
        Lexer a = new Lexer(h5);
        List<Token> tokens = a.ScanTokens();
        // foreach (Token t2 in tokens)
        // {

        //     Console.WriteLine(t2.type);
        // }
        Parser parser = new Parser(tokens);
        Node effect = parser.Parse();
        Console.WriteLine("Parsed Effect:");
        effect.Show();
        SemanticNew semanticNew = new SemanticNew();
        semanticNew.CheckNode(effect);
    }
}