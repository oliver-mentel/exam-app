using System.Collections.Generic;
using ExamApp.Context;

namespace ExamApp.Services;

public class LanguageService
{
    public IAsyncEnumerable<Language> GetLanguages()
    {
        MainContext ctx = new MainContext();

        return ctx.Languages.AsAsyncEnumerable();
    }
}