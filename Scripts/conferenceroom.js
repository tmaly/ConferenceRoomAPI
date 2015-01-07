function loadSchedule(date, mbx) {

    try {

        // date must have form mm-dd-yyyy

        var _url = '/api/ConferenceRoom';

        //var _url = 'http://conferenceroomapi9194.azurewebsites.net/api/ConferenceRoom';
        //var _url = 'http://localhost:50093/api/ConferenceRoom';
        _url += '/' + mbx;
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
                        alert(calEvent.title + ': '
                        + calEvent.start.format("h:mm a")
                        + ' to '
                        + calEvent.end.format("h:mm a"));
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

        var room = getMbx();
        if (!room) {
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

                var attr = resp.RoomAttributes;
                var data = attr.Name + '\r\n';
                data += 'Capacity:   ' + attr.Capacity + '\r\n';
                if (attr.PhoneNumber) {
                    data += 'Phone #:   ' + attr.PhoneNumber + '\r\n';
                }
                data += 'White Board:   ' + attr.WhiteBoard + '\r\n';
                data += 'Projector:   ' + attr.AAV + '\r\n';
                //data += 'Major ID:   ' + attr.MajorID + '\r\n';
                //data += 'Minor ID:   ' + attr.MinorID + '\r\n';

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
        $.ajax({
            url: '/api/ConferenceRoom/window/' + currentDate,
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
    loadSchedule(getPrevDate(), getMbx());
});

$('#future').click(function (e) {
    loadSchedule(getNextDate(), getMbx());
});

$('#room').click(function (e) {
    loadRoomInfo();
});

function getDate() {
    var div_cdte = $("div#data_cdte")[0];
    var date = jQuery.data(div_cdte, "cdte");
    return date.current;
}

function getMbx() {
    var div_mbx = $("div#data_mbx")[0];
    var mbx = jQuery.data(div_mbx, "mbx");
    return mbx.current;
}

function setDate(dte) {
    var div_tid = $("div#data_cdte")[0];
    jQuery.data(div_tid, "cdte", { current: dte });
}

function setMbx(mbx) {
    var div_mbx = $("div#data_mbx")[0];
    jQuery.data(div_mbx, "mbx", { current: mbx });
}

function init(date, mbx) {
    $('#nav').hide();

    if ($('#data_cdte').length == 0)
        $('body').append('<div id="data_cdte">');

    if ($('#data_nbx').length == 0)
        $('body').append('<div id="data_mbx">');

    if ($('#prev').length == 0)
        $('body').append('<div id="prev">');

    if ($('#next').length == 0)
        $('body').append('<div id="next">');

    setDate(date);
    setMbx(mbx);
    loadSchedule(date, mbx);
}

function getPrevDate() {
    var date = jQuery.data($("div#prev")[0], "prev");
    return date.current;
}

function getNextDate() {
    var date = jQuery.data($("div#next")[0], "next");
    return date.current;
}

$('#refresh').click(function (e) {
    var room = $('#SelectedRoom').val();
    setMbx(room)
    loadSchedule(getDate(), room);
});

