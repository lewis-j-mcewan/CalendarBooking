# Calendar Booking Console Application
This application is designed so a user can manage appointments within a 2024 calendar.
Each booking timeslot is only 30mins, and any booking desired to be longer than this will need to book 2 or more slots .
Booking constraints include:
- A booking can only be made between 9am-5pm. This means the last booking of the day will end at 5:30pm.
- A booking cannot be made between 4pm-5pm on the second day of the third week of any month.

The application will accept the following commands in the command line:
- `ADD DD/MM hh:mm` to add an appointment to the calendar at the specified date and time.
- `DELETE DD/MM hh:mm` to delete an existing appointment from the calendar.
- `FIND DD/MM` to find the next available appointment slot on the given day.
- `KEEP hh:mm` to book out a specific time on every day of the calendar year.

The results of the booking attempt will be printed to the command line.

## How to Run
There are two components to required to run this application.

1. Run the `docker-compose.yml` file to create the mssql database. This is where all appointments will be stored.
2. Run the `CalendarBooking` Console Application. Upon startup, this will create the database table and seed it with 
bookings for 4-5pm on the second day of the third week in every month. This prevents subsequent bookings from being 
made at this time.

## Future Improvements
- At present the calendar only functions for the 2024 calendar year. In future this would be able to handle any year.
- No user is assigned to any booking. If there were a user assigned then it is possible to see which booking belongs to 
the client and which booking belongs to the business. </br>
For example: the bookings automatically created between 4-5pm on the second day of the third week of every month belong 
to the business, but there is no way to identify that once they are added.
- As this application is only run locally, and will not start unless it can connect to the database. 
Were this to be deployed, there is a possibility the database operations may fail or block threads. An improvement here 
would be to add exception handling around database calls and make them asynchronous. This will provide greater safety 
and speed. 
- Being a console application, this application can only be consumed by a single user running it. Converting this to an 
API would allow clients to add and manage their own bookings. Additionally, more cross-cutting concerns such as 
logging would be implemented to remove the reliance on console logs. 