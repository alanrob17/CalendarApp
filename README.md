# Google Calendar Application

This application takes the content from a Google Calendar in JSON format and reformats it into a web page.

It works on a start and end date and can create a historical or current web page. I am using it to record trips to Europe that I recorded in my Calender.

**Note:** I haven't been able to programmatically capture the event data. I am going to ``developer.google.com`` to run queries and grab my complete Calendar event data.

## Getting the Calendar event data

Got to the web page.

[Google developers website](https://developers.google.com/calendar/api/v3/reference/events/list).

### Inputs

* **calendarId** (your Google email address).
* **maxResults** (= 10,000 to get all Calendar events).

Now you can run the process. Once the process has completed with a result of ``200`` you can copy all of the data and save it in a file named, **yourFirstNameCalendar.json**.

## Running the console CalendarApp

There are a couple of inputs you need to set before running the program.

### Inputs

#### In Calendar.Program.Main()

Set **Name** to your user name that is stored in ``resource.resx``. Mine is User1.

Set the Date range you require for the web page.

```csharp
    // Get the start and end dates of the range
    var startDate = new DateTime(2013, 06, 09);
    var endDate = new DateTime(2013, 08, 25);
```

#### In Calendar.Data.FormatData.ReformatEventsToHtml()

Set **header** to the header you require that is stored in ``resource.resx``. Mine is ``AlanHeader``.

Once you run this process the Web page will be stored as **Itinerary.html**.

**Note:** this is just the Web page content and you will have to build a HTML header and footer to go around the content. I use Bootstrap to build a responsive web page.
