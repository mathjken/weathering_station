# weathering_station
GROUP COMPONENT
Students are expected to work in self-organising group to develop the group component,
bringing their developed microservice to the application. This component includes the
application development, and technical report with the logbook of group work and peer
assessment forms as appendices, as well as the group demonstration.
Application Development
For the group component, you will design and develop a modular front-end web application
that consumes the microservices of the individual components. Each group member will
develop a front-end component that will interact with their individual microservice, interacting
with and presenting data to the front-end application. The web application should provide user-
friendly dashboard for visualising data collected by the different monitoring stations, where
each monitoring station will be named after a region in the UK (North East, North West,
Yorkshire, East Midlands, West Midlands, Midlands, East of England, London, South East,
South West).
The developed solution should fulfil the following features:
- User authentication and authorisation – Implement user authentication and
authorisation for secure access to the web interface. This should include the creation,
deletion of users, as well as change password.
- Data View – Implement data view to display historical data of the different monitoring
stations. Each monitoring station should be presented separately – each student is
responsible for developing the UI for their monitoring station.
- Real-time updates Dashboard – Implement a dashboard to display real-time updates
and alerts derived from the different monitoring workstations microservices.
- Data Processing – Implement data processing module, performing analysis and
aggregation for the data derived from the different microservices. This should include
daily and monthly averages of each parameter of all areas.
- Data Visualisation – Create interactive data visualisation (e.g., charts) for presenting
aggregated data trends.
The developed solution should fulfil the following technical requirements:
- Development in C# using any code editor or IDE (e.g. Microsoft Visual Studio,
Microsoft Visual Code).
- UI design should be modular.
- Use RESTful APIs for communication between the different microservices of the
monitoring station and front-end UI. No communication should occur between the
front-end and the sensors APIs.
- Data should be derived from the monitoring station microservice, without storing the
same data in the application.
- Implementing error and exception handling.
- Testing the web application to ensure functionality and reliability.
