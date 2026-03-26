using ExamPortal.Api.Models;

namespace ExamPortal.Api.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Questions.Any())
            return;

        var questions = new List<Question>
        {
            new Question
            {
                Text = "ฟิลด์เลขคี่จากตัวเลือกนี้",
                OrderNo = 1,
                Choices = new List<Choice>
                {
                    new Choice { Text = "3", IsCorrect = false, Score = 0 },
                    new Choice { Text = "5", IsCorrect = false, Score = 0 },
                    new Choice { Text = "9", IsCorrect = true, Score = 1 },
                    new Choice { Text = "11", IsCorrect = false, Score = 0 }
                }
            },
            new Question
            {
                Text = "2 x 2 = 4 จะมากกว่า x",
                OrderNo = 2,
                Choices = new List<Choice>
                {
                    new Choice { Text = "1", IsCorrect = false, Score = 0 },
                    new Choice { Text = "2", IsCorrect = true, Score = 1 },
                    new Choice { Text = "3", IsCorrect = false, Score = 0 },
                    new Choice { Text = "4", IsCorrect = false, Score = 0 }
                }
            }
        };

        context.Questions.AddRange(questions);
        context.SaveChanges();
    }
}