function loadSchedule(date) {

    try {

        var room = $('#SelectedRoom').val();

        if (!room) {
            alert("Please select Conf. Room");
            return;
        }

        var _url = '/api/ConferenceRoom';

        //var _url = 'http://conferenceroomapi9194.azurewebsites.net/api/ConferenceRoom';
        //var _url = 'http://localhost:50093/api/ConferenceRoom';
        _url += '/' + room;
        _url += '/schedule/' + date;

        $('#overlay').addClass('loadingOverlay')

        $.ajax({
            url: _url,
            type: 'GET',
            dataType: 'json',
            success: function (resp) {

                var schedule = [];
                $.each(resp, function (index, value) {
                    schedule.push({ title: value.Subject, start: value.StartDate, end: value.EndDate });
                });

                setPrevNext(date);

                $('#nav').show();
                $('#overlay').removeClass('loadingOverlay')
                $('#calendar').fullCalendar('destroy');
                $('#calendar').fullCalendar({
                    editable: false,
                    defaultDate: date,
                    timezone: 'local',
                    height: 600,
                    defaultView: 'agendaDay',
                    header: {
                        left: 'title',
                        center: '',
                        right: ''
                    },
                    eventClick: function (calEvent, jsEvent, view) {

                        var display = calEvent.title + ': ';
                        display += calEvent.start.format("h:mm a");
                        display += ' to ';
                        display += calEvent.end.format("h:mm a");
                        alert(display);
                    },
                    events: schedule
                });

            },
            error: function (jqxhr, textstatus) {
                alert('Unable to Get CRs: ' + textstatus);
                $('#overlay').removeClass('loadingOverlay')
            }
        });
    } catch (e) {
        alert(e.message);
    }
}

function loadRoomInfo() {

    try {

        var room = $('#SelectedRoom').val();

        if (!room) {
            alert("Please select Conf. Room");
            return;
        }

        var _url = '/api/ConferenceRooms';
        _url += '/' + room + '/information'

        $.ajax({
            url: _url,
            type: 'GET',
            dataType: 'json',
            success: function (resp) {

                // {"roomAttributes":{"mailBox":"SoxConferenceRoom@sprcompanies.com","capacity":10,"phoneNumber":null,"aav":true,"whiteBoard":true}}

                var data = 'Capacity:   ' + resp.RoomAttributes.Capacity + '\r\n'; 
                data += 'Phone #:   ' + resp.RoomAttributes.PhoneNumber + '\r\n';
                data += 'White Board:   ' + resp.RoomAttributes.WhiteBoard + '\r\n';
                data += 'AAV:   ' + resp.RoomAttributes.Aav + '\r\n';
                data += 'Major ID:   ' + resp.RoomAttributes.MajorID + '\r\n';
                data += 'Minor ID:   ' + resp.RoomAttributes.MinorID + '\r\n';

                alert(data);

            },
            error: function (jqxhr, textstatus) {
                alert('Unable to Get Conference Room Information: ' + textstatus);
            }
        });
    } catch (e) {
        alert(e.message);
    }
}

function setPrevNext(currentDate) {

    try {

        var _url = '/api/ConferenceRoom/window/' + currentDate;

        $.ajax({
            url: _url,
            type: 'GET',
            dataType: 'json',
            success: function (resp) {

                if (resp) {
                    jQuery.data($("div#prev")[0], "prev", { current: resp.prev });
                    jQuery.data($("div#next")[0], "next", { current: resp.next });
                }

            },
            error: function (jqxhr, textstatus) {
                alert('Unable to Get Date Window: ' + textstatus);
            }
        });
    } catch (e) {
        alert(e.message);
    }
}

$('#past').click(function (e) {
    loadSchedule(getPrevDate());
});

$('#room').click(function (e) {
    loadRoomInfo();
});

$('#future').click(function (e) {
    loadSchedule(getNextDate());
});

function getPrevDate() {
    var date = jQuery.data($("div#prev")[0], "prev");
    return date.current;
}

function getNextDate() {
    var date = jQuery.data($("div#next")[0], "next");
    return date.current;
}

function init(date) {
    $('#nav').hide();

    if($('#data_cdte').length == 0)
        $('body').append('<div id="data_cdte">');
            
    if ($('#prev').length == 0)
        $('body').append('<div id="prev">');

    if ($('#next').length == 0)
        $('body').append('<div id="next">');

    setDate(date)
}

function getDate() {
    var div_cdte = $("div#data_cdte")[0];
    var date = jQuery.data(div_cdte, "cdte");
    return date.current;
}

function setDate(dte) {
    var div_tid = $("div#data_cdte")[0];
    jQuery.data(div_tid, "cdte", { current: dte });
}

$('#refresh').click(function (e) {
    loadSchedule(getDate());
});
