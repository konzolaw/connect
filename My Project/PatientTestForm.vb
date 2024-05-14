Imports System.IO
Imports MySql.Data.MySqlClient

Public Class PatientTestForm


    Private Sub ButtonSavePatientDetails_Click(sender As Object, e As EventArgs)
        ' Check if any textbox is empty
        If String.IsNullOrWhiteSpace(TextBoxPatientName.Text) OrElse
           String.IsNullOrWhiteSpace(TextBoxPhoneNumber.Text) OrElse
           String.IsNullOrWhiteSpace(TextBoxAge.Text) OrElse
           String.IsNullOrWhiteSpace(TextBoxHomeaddress.Text) OrElse
          String.IsNullOrWhiteSpace(TextBoxBloodpressure.Text) OrElse
           String.IsNullOrWhiteSpace(TextBoxHeight.Text) OrElse
           String.IsNullOrWhiteSpace(TextBoxWeight.Text) OrElse
           String.IsNullOrWhiteSpace(TextBoxTestConcerns.Text) OrElse
           String.IsNullOrWhiteSpace(TextBoxAllergies.Text) OrElse
           String.IsNullOrWhiteSpace(TextBoxCurrentmedication.Text) OrElse
           String.IsNullOrWhiteSpace(TextBoxHeartbeat.Text) OrElse
           String.IsNullOrWhiteSpace(TextBoxTemperature.Text) Then
            ' Display error message if any field is empty
            MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else

            ' Display success message
            MessageBox.Show("Patient details successfully saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' Optionally, you can add code here to save the patient details to your database or perform any other actions
        End If
    End Sub



    Private Sub RefferalButton_Click(sender As Object, e As EventArgs) Handles RefferalButton.Click
        ' Hide the current PatientTestForm
        Me.Hide()

        ' Create a new instance of AppointmentForm and show it
        Dim RefferalForm As New RefferalForm()
        RefferalForm.Show()
    End Sub

    Private Sub ButtonMakePurchase_Click(sender As Object, e As EventArgs) Handles ButtonMakePurchase.Click
        ' Display a message indicating that this system version only performs referrals
        MessageBox.Show("Sorry, This System Version only Performs Referrals.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub LinkLabelHomeEsc_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabelHomeEsc.LinkClicked
        ' Create an instance of the PatientRegistry form
        Dim PatientRegistry As New PatientsRegistry()

        ' Show the desired tab (TabPage1)
        PatientRegistry.PatientRegistryTabControl.SelectedTab = PatientsRegistry.PatientQueueList

        ' Show the PatientRegistry form and hide the current form
        PatientsRegistry.Show()
        Me.Hide()
    End Sub

    Private Sub LinkLabelEscAppointment_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabelEscAppointment.LinkClicked
        ' Create an instance of RefferalForm
        Dim RefferalForm As New RefferalForm()

        ' Show RefferalForm and hide the current form (PatientsRegistry)
        RefferalForm.Show()
        Me.Hide()
    End Sub



    Dim connectionString As String = "server=localhost;database=pharmacydb;user=root;password=Irke@12843"


    Private _rowIndex As Integer
    Private _patientUPI As String

    ' Constructor to initialize the form with row index and patient UPI
    Public Sub New(rowIndex As Integer, patientUPI As String)
        InitializeComponent()
        _rowIndex = rowIndex
        _patientUPI = patientUPI
    End Sub

    ' Load event handler for the form
    Private Sub PatientTestForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Perform data retrieval and update UI based on the row index and patient UPI
            ' Open database connection
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                ' Fetch required data from Patients, MedicalDetails, Queues, and LabTests tables
                Dim query As String = "SELECT patients.FirstName, patients.LastName, patients.PhoneNumber, patients.HomeAddress, patients.DateOfBirth, medicaldetails.BloodPressure, medicaldetails.HeartbeatRate, medicaldetails.Height, medicaldetails.Weight, medicaldetails.Allergies, medicaldetails.CurrentMedication, medicaldetails.Temperature, queues.DepartmentFrom, queues.QueueStatus, labtests.LabTechnicianName, labtests.LabTechnicianID, labtests.TestNumbers, labtests.TestConcerns, labtests.Prescription, labtests.TestDiagnosis, labtests.MainNotes " &
                             "FROM Patients  " &
                             "INNER JOIN MedicalDetails ON patients.PatientUPI = medicaldetails.PatientUPI " &
                             "INNER JOIN Queues  ON patients.PatientUPI = queues.PatientUPI " &
                             "INNER JOIN LabTests  ON patients.PatientUPI = labtests.PatientUPI " &
                             "WHERE patients.PatientUPI = @PatientUPI"
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@PatientUPI", _patientUPI)

                    Using reader As MySqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            ' Retrieve data from the reader with handling of null values
                            Dim firstName As String = If(reader("FirstName") IsNot DBNull.Value, reader("FirstName").ToString(), String.Empty)
                            Dim lastName As String = If(reader("LastName") IsNot DBNull.Value, reader("LastName").ToString(), String.Empty)
                            Dim patientName As String = $"{firstName} {lastName}"
                            Dim phoneNumber As String = If(reader("PhoneNumber") IsNot DBNull.Value, reader("PhoneNumber").ToString(), String.Empty)
                            Dim homeAddress As String = If(reader("HomeAddress") IsNot DBNull.Value, reader("HomeAddress").ToString(), String.Empty)
                            Dim dateOfBirth As Date? = If(reader("DateOfBirth") IsNot DBNull.Value, Date.Parse(reader("DateOfBirth").ToString()), Nothing)

                            ' Calculate age from date of birth and current date if date of birth is not null
                            Dim age As Integer = If(dateOfBirth.HasValue, CalculateAge(dateOfBirth.Value, Date.Today), 0)

                            ' Fetch additional data with handling of null values
                            Dim bloodPressure As String = If(reader("BloodPressure") IsNot DBNull.Value, reader("BloodPressure").ToString(), String.Empty)
                            Dim heartbeatRate As String = If(reader("HeartbeatRate") IsNot DBNull.Value, reader("HeartbeatRate").ToString(), String.Empty)
                            Dim height As String = If(reader("Height") IsNot DBNull.Value, reader("Height").ToString(), String.Empty)
                            Dim weight As String = If(reader("Weight") IsNot DBNull.Value, reader("Weight").ToString(), String.Empty)
                            Dim allergies As String = If(reader("Allergies") IsNot DBNull.Value, reader("Allergies").ToString(), String.Empty)
                            Dim currentMedication As String = If(reader("CurrentMedication") IsNot DBNull.Value, reader("CurrentMedication").ToString(), String.Empty)
                            Dim temperature As String = If(reader("Temperature") IsNot DBNull.Value, reader("Temperature").ToString(), String.Empty)
                            Dim departmentFrom As String = If(reader("DepartmentFrom") IsNot DBNull.Value, reader("DepartmentFrom").ToString(), String.Empty)
                            Dim queueStatus As String = If(reader("QueueStatus") IsNot DBNull.Value, reader("QueueStatus").ToString(), String.Empty)
                            Dim labTechnicianName As String = If(reader("LabTechnicianName") IsNot DBNull.Value, reader("LabTechnicianName").ToString(), String.Empty)
                            Dim labTechnicianID As String = If(reader("LabTechnicianID") IsNot DBNull.Value, reader("LabTechnicianID").ToString(), String.Empty)
                            Dim testNumbers As String = If(reader("TestNumbers") IsNot DBNull.Value, reader("TestNumbers").ToString(), String.Empty)
                            Dim testConcerns As String = If(reader("TestConcerns") IsNot DBNull.Value, reader("TestConcerns").ToString(), String.Empty)
                            Dim Prescription As String = If(reader("Prescription") IsNot DBNull.Value, reader("Prescription").ToString(), String.Empty)
                            Dim testDiagnosis As String = If(reader("TestDiagnosis") IsNot DBNull.Value, reader("TestDiagnosis").ToString(), String.Empty)
                            Dim mainNotes As String = If(reader("MainNotes") IsNot DBNull.Value, reader("MainNotes").ToString(), String.Empty)

                            ' Update the UI with retrieved data
                            SetData(patientName, _patientUPI, phoneNumber, age, homeAddress, bloodPressure, heartbeatRate, height, weight, allergies, currentMedication, temperature, departmentFrom, queueStatus, labTechnicianName, labTechnicianID, testNumbers, testConcerns, Prescription, testDiagnosis, mainNotes)

                            ' Log message to indicate that data retrieval is successful
                            MessageBox.Show("Data retrieval successful.")
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Log message to indicate any errors that occur
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Method to update UI with retrieved data
    Private Sub SetData(patientName As String, patientUPI As String, phoneNumber As String, age As Integer, homeAddress As String, bloodPressure As String, heartbeatRate As String, height As String, weight As String, allergies As String, currentMedication As String, temperature As String, departmentFrom As String, queueStatus As String, labTechnicianName As String, labTechnicianID As String, testNumbers As String, testConcerns As String, Prescription As String, testDiagnosis As String, mainNotes As String)
        ' Set data to respective textboxes
        TextBoxPatientName.Text = patientName
        TextBoxPatientUPI.Text = patientUPI
        TextBoxPhoneNumber.Text = phoneNumber
        TextBoxAge.Text = age.ToString()
        TextBoxHomeaddress.Text = homeAddress
        TextBoxBloodpressure.Text = bloodPressure
        TextBoxHeartbeat.Text = heartbeatRate
        TextBoxHeight.Text = height
        TextBoxWeight.Text = weight
        TextBoxAllergies.Text = allergies
        TextBoxCurrentmedication.Text = currentMedication
        TextBoxTemperature.Text = temperature
        TextBoxDepartmentFrom.Text = departmentFrom
        TextBoxQueueStatus.Text = queueStatus
        TextBoxLabTechnicianName.Text = labTechnicianName
        TextBoxLabTechnicianID.Text = labTechnicianID
        TextBoxTestNumbers.Text = testNumbers
        TextBoxTestConcerns.Text = testConcerns
        TextBoxPrescription.Text = Prescription
        TextBoxTestDiagnosis.Text = testDiagnosis
        TextBoxMainNotes.Text = mainNotes
    End Sub

    Private Function CalculateAge(birthDate As Date, currentDate As Date) As Integer
        ' Calculate age
        Return currentDate.Year - birthDate.Year - If(currentDate.DayOfYear < birthDate.DayOfYear, 1, 0)
    End Function


End Class