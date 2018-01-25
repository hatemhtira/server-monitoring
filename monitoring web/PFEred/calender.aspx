<%@ import Namespace=" System.Data" %>

<%@ Page Title="Banque de Tunisie" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="calender.aspx.cs" Inherits="PFEred.calender" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenu" runat="server">
      
            <!-- Content -->
            
                <h4 class="page-title">CALENDAR</h4>
  
                <div class="col-md-8 clearfix">
                    
                    <div id="calendar" class="p-relative p-10 m-b-20">
                        <!-- Calendar Views -->
                        <ul class="calendar-actions list-inline clearfix">
                            <li class="p-r-0">
                                <a data-view="month" href="#" class="tooltips" title="Month">
                                    <i class="sa-list-month"></i>
                                </a>
                            </li>
                            <li class="p-r-0">
                                <a data-view="agendaWeek" href="#" class="tooltips" title="Week">
                                    <i class="sa-list-week"></i>
                                </a>
                            </li>
                            <li class="p-r-0">
                                <a data-view="agendaDay" href="#" class="tooltips" title="Day">
                                    <i class="sa-list-day"></i>
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
                
                
               
                
                <br/><br/>
           

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <!-- jQuery -->
        <script src="js/jquery.min.js"></script> <!-- jQuery Library -->
        <script src="js/jquery-ui.min.js"></script> <!-- jQuery UI -->

        <!-- Bootstrap -->
        <script src="js/bootstrap.min.js"></script>
        
        <!-- UX -->
        <script src="js/scroll.min.js"></script> <!-- Custom Scrollbar -->
        
        <!--  Form Related -->
        <script src="js/validation/validate.min.js"></script> <!-- jQuery Form Validation Library -->
        <script src="js/validation/validationEngine.min.js"></script> <!-- jQuery Form Validation Library - requirred with above js -->
        
        <!-- Other -->
        <script src="js/calendar.min.js"></script> <!-- Calendar -->
        <script src="js/feeds.min.js"></script> <!-- News Feeds -->
        
        
        <!-- All JS functions -->
        <script src="js/functions.js"></script>
        
        <script type="text/javascript">
            $(document).ready(function() {
                var date = new Date();
                var d = date.getDate();
                var m = date.getMonth();
                var y = date.getFullYear();
                
                $('#calendar').fullCalendar({
                    header: {
                         center: 'title',
                         left: 'prev, next',
                         right: ''
                    },

                    selectable: true,
                    selectHelper: true,
                    editable: false,
                    events: [
                       
                   <%   foreach (DataRow pRow in Log.Tables["dsServeur"].Rows)
            {

                t = (DateTime)pRow["temps"] ;
                msg = (string)pRow["Messagelog"];
                 D = new DateTime(t.Year, t.Month, t.Day, t.Hour, t.Minute, t.Second);
                %>
                    {
                            title:' <% =msg.ToString() %>',
                            start: new Date(<%= D.Year.ToString()%>, <%=( D.Month-1).ToString() %>, <%=D.Day.ToString()%>,<%= D.Hour.ToString()%>,<%= D.Minute.ToString()%>,<%= D.Second.ToString()%>),
                            allDay:false
                            
                        },
          <%  }%>  

                        
                    ],
                     
                    //On Day Select
                    select: function(start, end, allDay) {
                        $('#addNew-event').modal('show');   
                        $('#addNew-event input:text').val('');
                        $('#getStart').val(start);
                        $('#getEnd').val(end);
                    },
                     
                    eventResize: function(event,dayDelta,minuteDelta,revertFunc) {
                        $('#editEvent').modal('show');

                        var info =
                            "The end date of " + event.title + "has been moved " +
                            dayDelta + " days and " +
                            minuteDelta + " minutes."
                        ;
                        
                        $('#eventInfo').html(info);
                
                
                        $('#editEvent #editCancel').click(function(){
                             revertFunc();
                        }) 
                    }
                });
                
                $('body').on('click', '#addEvent', function(){
                     var eventForm =  $(this).closest('.modal').find('.form-validation');
                     eventForm.validationEngine('validate');
                     if (!(eventForm).find('.formErrorContent')[0]) {
                          
                          //Event Name
                          var eventName = $('#eventName').val();

                          //Render Event
                          $('#calendar').fullCalendar('renderEvent',{
                               title: eventName,
                               start: $('#getStart').val(),
                               end:  $('#getEnd').val(),
                               allDay: true,
                          },true ); //Stick the event
                          
                          $('#addNew-event form')[0].reset()
                          $('#addNew-event').modal('hide');
                     } 
                });   
            });    
            
            //Calendar views
            $('body').on('click', '.calendar-actions > li > a', function(e){
                e.preventDefault();
                var dataView = $(this).attr('data-view');
                $('#calendar').fullCalendar('changeView', dataView);
                
                //Custom scrollbar
                var overflowRegular, overflowInvisible = false;
                overflowRegular = $('.overflow').niceScroll();     
            });                    
            
        </script>
  
</asp:Content>
