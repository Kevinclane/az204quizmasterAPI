local connectionString format: Server="localhost";Port=3306;Database="{database}";UserID="root";Password="{password}";

add migration: dotnet ef migrations add {name}
update database: dotnet ef database update


JsonIntake format: {
    Question: string,
    QuestionType: int/enum, //MultipleChoiceSingle=0, MultipleChoiceMultiple=1, Match=2
    ResourceLink?: string, //url to related Microsoft learning page
    Image?: string, //url to image related to question
    Category: int/enum, //ComputeSolutions=0, Storage=1, Security=2, Monitor=3, ThirdParty=4,
    OptionIntakes: [
        {
            LeftDisplay: string, //display for possible answer to Question. Default display
            RightDisplay: string?, //display for right side. Only needed for Match questionType
            IsCorrect?: boolean //declare if this option is the correct answer to the question. Only needed for Multiple Choice
        }
    ]
}

All questions must have at least 2 options.
At least 1 option must be corrrect for MultipleChoice questionType.