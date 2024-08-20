local connectionString format: Server="localhost";Port=3306;Database="{database}";UserID="root";Password="{password}";

add migration: dotnet ef migrations add {name}
update database: dotnet ef database update


JsonIntake format: {
    Question: string,
    QuestionType: int/enum, //MultipleChoiceSingle=0, MultipleChoiceMultiple=1, Match=2
    Image?: string, //url to image related to question
    Category: int/enum, //ComputeSolutions=0, Storage=1, Security=2, Monitor=3, ThirdParty=4,
    Options: [
        {
            LeftDisplay: string, //display for possible answer to Question. Default display
            RightDisplay: string?, //display for right side. Only needed for Match questionType
            IsCorrect?: boolean //declare if this option is the correct answer to the question. Only needed for Multiple Choice
        }
    ],
    ResourceLinks: string[] //list of urls related to question topic
}

All questions must have at least 2 options.
Exactly 1 Option must be correct for MultipleChoiceSingle
At least 1 Option must be correct for MultipleChoiceMultiple

Blank:
{
        "Question": "",
        "QuestionType": n,
        "Category": n,
        "Options": [
            {
                "LeftDisplay": "",
                "IsCorrect": false
            },
            {
                "LeftDisplay": "",
                "IsCorrect": false
            },
            {
                "LeftDisplay": "",
                "IsCorrect": false
            },
            {
                "LeftDisplay": "",
                "IsCorrect": false
            }
        ],
        "ResourceLinks": []
    }


