ConferenceRoomAPI
=================

This is Web API project that wraps an EWS feed to Office365.

The API is defined as - 

(All Get)

1. /api/ConferenceRooms

	Get all confgi. CR mailboxes (listed above)

	Response:
  {
      "conferenceRooms": [
          {
              "mailBox": "CubsConferenceRoom@sprcompanies.com",
              "capacity": 8,
              "phoneNumber": null,
              "aav": true,
              "whiteBoard": true,
              "majorID": 1,
              "minorID": 1
          },
        ...]
  }

2. /api/ConferenceRooms/{id}/Information

	Get info. about a CR (capacity, contains whiteboard, etc.)

	Response: 
  {
      "roomAttributes": {
          "mailBox": "SoxConferenceRoom@sprcompanies.com",
          "capacity": 10,
          "phoneNumber": null,
          "aav": true,
          "whiteBoard": true,
          "majorID": 1,
          "minorID": 2
      }
  }

3. /api/conferenceroom/{id}/schedule/today

	CR schedule for current date

4. /api/conferenceroom/{id}/schedule/{date}

	CR schedule for specified date (NOTE: date format must be yyyy-mm-dd)

5. /api/conferenceroom/{id}/schedule/{startDate}/{endDate}

	CR schedule for specified date range (NOTE: date format must be yyyy-mm-dd)

All /schedule/ API calls return the following JSON -  
[
    {
        "organizer": "Karen Sims",
        "subject": "Karen Sims ",
        "eventType": "Single",
        "duration": "01:00:00",
        "startDate": "2014-11-11T15:00:00+00:00",
        "endDate": "2014-11-11T16:00:00+00:00",
        "timeRange": "3:00 PM to 4:00 PM"
    },
    {
        "organizer": "Rebecca Butman",
        "subject": "Rebecca Butman ",
        "eventType": "Single",
        "duration": "00:30:00",
        "startDate": "2014-11-11T16:30:00+00:00",
        "endDate": "2014-11-11T17:00:00+00:00",
        "timeRange": "4:30 PM to 5:00 PM"
    },
    {
        "organizer": "Lorena Sinjari",
        "subject": "Lorena Sinjari ",
        "eventType": "Single",
        "duration": "02:00:00",
        "startDate": "2014-11-11T18:00:00+00:00",
        "endDate": "2014-11-11T20:00:00+00:00",
        "timeRange": "6:00 PM to 8:00 PM"
    }
    ...
]



