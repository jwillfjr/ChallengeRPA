using Domain.Entities;
using Domain.IRPA;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Security.Cryptography;

namespace RPA
{
    public class Search : ISearch
    {
        private readonly ChromeDriver driver;
        private string _term = string.Empty;
        private readonly string url = "https://www.alura.com.br/";
        public Search()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--headless");
            driver = new ChromeDriver(options);
        }

        private async Task GoToUrl()
        {
            try
            {
                await driver.Navigate().GoToUrlAsync(url);
            }
            catch (Exception e)
            {
                throw new Exception($"Error when trying to navigate: {url} ", e);
            }

        }

        private IWebElement? FindElement(By by, bool ignore = false, int waitTime = 5)
        {
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(waitTime));
            
            try
            {
                return wait.Until((d) =>
                  {
                      return d.FindElement(by);
                  });
            }
            catch
            {
                if (!ignore)
                    throw new Exception($"Element {by.ToString()} not found!");
                else
                    return null;
            }

        }
        private ReadOnlyCollection<IWebElement> FindElements(By by, int waitTime = 5)
        {
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(waitTime));
            return wait.Until((d) =>
            {
                try
                {
                    return d.FindElements(by);
                }
                catch
                {
                    throw new Exception($"Element {by.ToString()} not found!");
                }
            });
        }
        private void FilterSearch()
        {
            try
            {
                var inp = FindElement(By.Id("header-barraBusca-form-campoBusca"));
                inp?.GetAttribute("value");
                inp?.SendKeys(_term);
                inp?.Submit();
                FindElement(By.ClassName("show-filter-options"))?.Click();
                FindElement(By.XPath("/html/body/div[2]/div[1]/div[2]/ul/li[1]/label"))?.Click();
                FindElement(By.Id("busca--filtrar-resultados"))?.Submit();
            }
            catch (Exception e)
            {
                throw new Exception($"Error executing FilterSearch method: {e.Message}", e);
            }
        }

        private string[] GetLinks()
        {
            try
            {
                var elemts =  FindElements(By.ClassName("busca-resultado-link"),10).Select(link =>
                {
                    return link.GetAttribute("href");
                }).ToList();
                var btnNext = FindElement(By.XPath("/html/body/div[2]/div[2]/nav/a[2]"), true);
                if (btnNext != null && !btnNext.GetAttribute("class").Contains("disabled"))
                {
                    btnNext.Click();
                    elemts.AddRange(GetLinks());
                }
                return  elemts.ToArray();
            }
            catch (Exception e)
            {
                throw new Exception($"Error executing GetLinks method: {e.Message}", e);
            }
        }

        public async Task<Course[]> GetCourses(string term)
        {
            try
            {
                _term = term;
                await GoToUrl();
                FilterSearch();
                var links = GetLinks();
                return await GetCourses(links);
            }
            finally
            {
                this.Dispose();
            }
        }
        private async Task<Course[]> GetCourses(string[] links)
        {
            try
            {
                var courses = new List<Course>();
                foreach (string link in links)
                {
                    await driver.Navigate().GoToUrlAsync(link);
                    string workload = FindElement(By.ClassName("courseInfo-card-wrapper-infos"), true)?.Text ?? string.Empty;
                    string description = FindElement(By.ClassName("course-list"), true)?.Text ?? string.Empty;
                    string teacher = FindElement(By.ClassName("instructor-title--name"), true)?.Text ?? string.Empty;
                    string title = FindElement(By.ClassName("curso-banner-course-title"), true)?.Text ?? string.Empty;
                    title = $"{title}{FindElement(By.ClassName("course--banner-text-category"), true)?.Text ?? string.Empty}";
                    courses.Add(new Course
                    {
                        Description = description,
                        Teacher = teacher,
                        Title = title,
                        Workload = workload
                    });
                }
                return [.. courses];
            }
            catch (Exception e)
            {
                throw new Exception($"Error executing GetCourses method: {e.Message}", e);
            }

        }

        public void Dispose()
        {
            driver.Quit();
            GC.SuppressFinalize(this);
        }
    }
}
