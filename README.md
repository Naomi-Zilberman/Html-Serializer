# HTML Serializer Project

This project develops a tool for processing and manipulating HTML, useful for tasks like web crawling.

## Project Description

This project develops a tool to process and manipulate HTML. Such a tool can be used for various purposes, for example, to implement a Crawler. A Crawler (or Scraper) is a mechanism that reads websites and parses their HTML to extract the desired information. In fact, this is exactly what the Google search engine (and others) does. The engine crawls the internet and parses the HTML of all websites to index the information so that it can retrieve it according to the search queries that users request.

Other examples of using Crawling:

* Analyzing websites to discover which technologies they are written in and which code libraries they use.
* Extracting data from shopping or second-hand websites to display on another website.
* And much more.

In this project, we will develop the foundational code that we can later use to develop our own Crawler. The tool we develop consists of two parts: Html Serializer and Html Query.

## Html Serializer

Serialization is the process of converting data from a specific format to objects of a programming language. You need to develop a service that accesses a website address, reads the HTML that returns from it, and converts the HTML to C# objects.

### Development Steps

1.  **Reading a Web Page:**
    * Use the HttpClient object to make a call to a web address.
    * Example Code (C#):

    ```csharp
    public async Task<string> Load(string url) {
        HttpClient client = new HttpClient();
        var response = await client.GetAsync(url);
        var html = await response.Content.ReadAsStringAsync();
        return html;
    }
    ```

    * After calling the web address, you will receive a string containing all the HTML.
2.  **Splitting by Tags:**
    * Split the string into parts.
    * Use Regular Expressions that search for an HTML tag structure (starts with '<' and ends with '>').
    * Clean all empty strings, line breaks, and unnecessary spaces.
3.  **HtmlElement Class:**
    * Create a class that represents an HTML tag.
    * The class will contain the following properties:
        * Id
        * Name
        * Attributes (List)
        * Classes (List)
        * InnerHtml
    * Additionally, the class will contain Parent and Children properties that will allow you to create a tree of objects.
4.  **HtmlHelper Class:**
    * Develop a helper class that provides you with a list of HTML tags in a convenient way.
    * Use JSON files containing a list of tags. One contains all existing HTML tags, and the other contains tags that do not require tag closing.
    * HtmlHelper class will contain two string array properties.
    * In the Constructor, load the data from the JSON files into the arrays.
    * Read the file content using the File class.
    * Convert the data to an array using the Deserialize function of the JsonSerializer class (add using System.Text.Json).
5.  **Singleton:**
    * The HtmlHelper class is a classic case that fits the Singleton design pattern.
    * Singleton is intended for cases where we want to limit the number of class instances to only one.
6.  **Building the Tree:**
    * Create the root object and an additional variable that will hold the current element in each iteration.
    * Loop through the list of strings and in each iteration perform the following process:
        * Cut the first word in the string and check:
            * If it is "html/", it means you have reached the end of the HTML.
            * If it starts with "/", it means it is a closing tag - go to the previous level in the tree (i.e., put the parent in the current element).
            * If it is the name of an HTML tag, create a new object and add it to the Children list of the current element.
            * Parse the rest of the string (without the first word) and create the Attributes list.
            * If there is an Attribute named class, split it into parts by space (because multiple classes can be defined on an HTML element) and update the Classes property accordingly.
            * Set the Name property to the tag name and the Id to the identifier (from the attributes, if any).
            * Also, update the Parent accordingly.
            * Additionally, check if the tag is self-closing - if the string ends with "/" or the tag is in the list of tags that do not require a closing tag.
            * If it is not any of the above options, it means it is internal text of the element. Update the InnerHtml property of the current element with the string value.

## Html Query

Develop a service to query a tree of HtmlElement objects and search for elements by CSS Selector.

### Basic Search Implementation

* Search by tag name, id, and class name.
* Query rules:
    * To search by tag, concatenate the tag name, e.g., "div".
    * To search by id, concatenate the id with a prefix of #, e.g., "#mydiv".
    * To search by class, concatenate the class name with a prefix of . (dot), e.g., ".class-name".
    * Concatenation of several selectors without a space indicates searching for elements that meet all conditions, e.g., "div#mydiv.class-name".
    * Space between selector parts indicates searching in the next generations of the element (deep), e.g., "div #mydiv .class-name".

### Development Steps

1.  **Selector Class:**
    * Create a class that represents a Selector.
    * The class will contain the following properties:
        * TagName
        * Id
        * Classes (List)
    * Additionally, the class will contain Parent and Child properties that will allow you to build a hierarchy of selectors.
2.  **Static Function in Selector Class:**
    * Write a static function in the Selector class that converts a query string to a Selector object.
    * Split the string into parts by spaces, so you get a collection of strings, each representing an additional level in the query.
3.  **HtmlElement Functions:**
    * Implement two functions to run on the tree in the HtmlElement class.
        * Descendants function.
        * Ancestors function.
4.  **Finding Elements in the Tree by Selector:**
    * Implement an extension function for the HtmlElement class that will receive a Selector and return a list of elements that meet the Selector criteria.
5.  **Preventing Duplicates Using HashSet:**
    * Use the HashSet class for the search result.

## Tracking and Checking During Development

To check if the list of elements that the function returns is indeed correct and valid, you can check in the browser on the website itself.

* Open the console and write the desired selector like this:
    * `$$("div .class-name")`
* You will receive the list of elements that meet the criteria, and you can compare the result.

## Important Note

Sometimes there will be a difference in the result that does not stem from a problem in the function. Many times there is JavaScript code that adds elements to the page beyond the basic HTML that returned from calling the website address. If the selector in the console returns more elements than the function, you can check against the original HTML by right-clicking on the website and selecting 'View Page Source' in the menu.

---

# פרויקט HTML Serializer

פרויקט זה מפתח כלי לעיבוד ומניפולציה של HTML, שימושי למשימות כמו סריקת אתרים.

## תיאור הפרויקט

פרויקט זה מפתח כלי לעיבוד ומניפולציה של HTML. ניתן להשתמש בכלי כזה לצרכים שונים, לדוגמה, כדי לממש Crawler. Crawler (או Scraper) הוא מנגנון שקורא אתרי אינטרנט ומנתח את ה-HTML שלהם כדי לחלץ ממנו את המידע הרצוי. למעשה, זה בדיוק מה שעושה מנוע החיפוש של גוגל (ואחרים). המנוע סורק את האינטרנט ומנתח את ה-HTML של כל האתרים במטרה לאנדקס את המידע כך שיוכל לאחזר אותו בהתאם לשאילתות החיפוש שהמשתמשים מבקשים.

דוגמאות נוספות לשימוש ב-Crawling:

* ניתוח אתרי אינטרנט כדי לגלות באיזה טכנולוגיות הם כתובים ובאיזה ספריות קוד הם משתמשים.
* שליפת נתונים מאתרי קניות או יד שניה כדי להציג באתר אחר.
* ועוד ועוד.

בפרויקט זה נפתח את הקוד התשתיתי שבהמשך נוכל להשתמש בו כדי לפתח Crawler משלנו.
הכלי שנפתח מורכב משני חלקים: Html Serializer ו-Html Query.

## Html Serializer

סריאליזציה היא תהליך של המרת נתונים מפורמט מסוים לאוביקטים של שפת תכנות. עליך לפתח שירות שניגש לכתובת של דף אינטרנט, קורא את ה-Html שחוזר ממנה וממיר את ה-Html לאוביקטים של #C.

### שלבי הפיתוח

1.  **קריאה לדף אינטרנט:**
    * השתמש באוביקט HttpClient כדי לבצע קריאה לכתובת אינטרנט.
    * דוגמת קוד (C#):

    ```csharp
    public async Task<string> Load(string url) {
        HttpClient client = new HttpClient();
        var response = await client.GetAsync(url);
        var html = await response.Content.ReadAsStringAsync();
        return html;
    }
    ```

    * לאחר הקריאה לכתובת האינטרנט, תקבל מחרוזת שמכילה את כל ה-Html.
2.  **פירוק לפי תגיות:**
    * פרק את המחרוזת לחלקים.
    * השתמש ב-Regular Expression שמחפש מבנה של תגית html (מתחילה ב-'>' ומסתיימת ב-'<').
    * נקה את כל המחרוזות הריקות, ירידות השורה והרווחים המיותרים.
3.  **מחלקת HtmlElement:**
    * צור מחלקה שמייצגת תגית של Html.
    * המחלקה תכיל את המאפיינים הבאים:
        * Id
        * Name
        * Attributes (רשימה)
        * Classes (רשימה)
        * InnerHtml
    * בנוסף, המחלקה תכיל מאפיינים של Parent ו-Children שיאפשרו לך ליצור עץ של אוביקטים.
4.  **מחלקת HtmlHelper:**
    * פתח מחלקת עזר שתספק לך את רשימת תגיות ה-HTML בצורה נוחה.
    * השתמש בקבצי JSON שמכילים רשימת תגיות. אחד מכיל את כל התגיות הקיימות ב-HTML והשני מכיל את התגיות שלא דורשות סגירה של התגית.
    * מחלקת HtmlHelper תכיל שני מאפיינים מסוג מערך של string.
    * ב-Constructor טען את הנתונים מקבצי ה-JSON למערכים.
    * קרא את תוכן הקובץ באמצעות מחלקת File.
    * המר את הנתונים למערך באמצעות הפונקציה Deserialize של מחלקת JsonSerializer (יש להוסיף using ל-System.Text.Json).
5.  **Singleton:**
    * מחלקת HtmlHelper היא מקרה קלאסי שמתאים לתבנית עיצוב של Singleton.
    * Singleton נועד למקרים שבהם אנחנו מעונינים להגביל את מספר המופעים של המחלקה לאחד בלבד.
6.  **בניית העץ:**
    * צור את אוביקט השורש וכן משתנה נוסף שיחזיק את האלמנט הנוכחי בכל איטרציה.
    * רוץ בלולאה על רשימת המחרוזות ובכל איטרציה בצע את התהליך הבא:
        * חתך את המילה הראשונה במחרוזת ובדוק:
            * אם היא "html/", סימן שהגעת לסוף ה-html.
            * אם היא מתחילה ב-"/", סימן שמדובר בתגית סוגרת - עליך לרמה הקודמת בעץ (כלומר, שים באלמנט הנוכחי את האבא).
            * אם היא שם של תגית מתגיות ה-Html, צור אוביקט חדש והוסף אותו לרשימת ה-Children של האלמנט הנוכחי.
            * פרק את המשך המחרוזת (ללא המילה הראשונה) וצור את רשימת ה-Attributes.
            * אם יש Attribute בשם class, פרק אותו לחלקים לפי רווח (כיון שניתן להגדיר כמה classים על אלמנט Html) ועדכן את המאפיין Classes בהתאם.
            * שים במאפיין Name את שם התגית וב-Id את המזהה (מה-attributes, אם יש).
            * כמו כן, עדכן את ה-Parent בהתאם.
            * בנוסף, בדוק אם התגית סוגרת את עצמה (self-closing) - אם המחרוזת מסתיימת ב-"/" או שהתגית נמצאת ברשימת התגיות שלא דורשות תגית סגירה.
            * אם היא לא אף אחת מהאפשרויות הנ"ל, סימן שמדובר בטקסט פנימי של האלמנט. עדכן את המאפיין InnerHtml של האלמנט הנוכחי בערך של המחרוזת.

## Html Query

פתח שירות לתשאול עץ של אוביקטי HtmlElement וחיפוש אלמנטים לפי CSS Selector.

### מימוש חיפוש בסיסי

* חפש לפי שם תגית, id ושם class.
* כללי השאילתה:
    * לחיפוש לפי תגית, שרשר את שם התגית, לדוגמה: "div".
    * לחיפוש לפי id, שרשר את ה-id עם תחילית של #, לדוגמה: "#mydiv".
    * לחיפוש לפי class, שרשר את שם ה-class עם תחילית של . (נקודה), לדוגמה: ".class-name".
    * שרשור של כמה סלקטורים ללא רווח מציין חיפוש אלמנטים שמקיימים את כל התנאים, לדוגמה: "div#mydiv.class-name".
    * רווח בין חלקי ה-selector מציין חיפוש בדורות הבאים של האלמנט (לעומק), לדוגמה: "div #mydiv .class-name".

### שלבי הפיתוח

1.  **מחלקת Selector:**
    * צור מחלקה שמייצגת Selector.
    * המחלקה תכיל את המאפיינים הבאים:
        * TagName
        * Id
        * Classes (רשימה)
    * בנוסף, המחלקה תכיל מאפיינים של Parent ו-Child שיאפשרו לך לבנות היררכיה של סלקטורים.
2.  **פונקציה סטטית במחלקת Selector:**
    * כתוב פונקציה סטטית במחלקת Selector שממירה מחרוזת של שאילתה לאוביקט Selector.
    * פרק את המחרוזת לחלקים לפי הרווחים, כך שתקבל אוסף של מחרוזות, שכל אחת מייצגת רמה נוספת בשאילתה.
3.  **פונקציות ב-HtmlElement:**
    * ממש שתי פונקציות לריצה על העץ במחלקת HtmlElement.
        * פונקציית Descendants.
        * פונקציית Ancestors.
4.  **מציאת אלמנטים בעץ לפי סלקטור:**
    * ממש פונקציית הרחבה למחלקת HtmlElement שתקבל Selector ותחזיר רשימת אלמנטים שעונים לקריטריונים של הסלקטור.
5.  **מניעת כפילויות באמצעות HashSet:**
    * השתמש במחלקת HashSet עבור תוצאת החיפוש.

## מעקב ובדיקה במהלך הפיתוח

כדי לבדוק אם רשימת האלמנטים שהפונקציה מחזירה לנו אכן נכונה ותקינה, ניתן לבדוק בדפדפן על האתר עצמו.

* פתח את ה-console ורשום את הסלקטור הרצוי כך:
    * `$$("div .class-name")`
* תקבל את רשימת האלמנטים שעונים לקריטריון, ותוכל להשוות את התוצאה.

## הערה חשובה

לעיתים יהיה הבדל בתוצאה שלא נובע מבעיה בפונקציה. פעמים רבות יש קוד javascript שמוסיף אלמנטים לדף מעבר ל-html הבסיסי שחזר מהקריאה לכתובת האתר. אם הסלקטור בקונסול מחזיר# Project Title
