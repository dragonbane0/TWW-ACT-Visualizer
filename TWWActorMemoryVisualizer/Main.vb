Imports System.IO
Imports System.IO.Pipes
Imports System.Text
Imports System.Security.Principal

Public Class Main

    Public pipeClient As NamedPipeClientStream
    Public Connected As Boolean = False
    Public CurrentSortingColumn As Integer = 0
    Public CurrentSortingOrder As SortOrder = System.Windows.Forms.SortOrder.Ascending

    Dim ListBoxWindowInstance As ListBoxWindow
    Dim PictureBoxWindowInstance As VisualBar
    Dim g As Graphics
    Dim FindSlotSize As Integer = -1

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ListBoxWindowInstance = New ListBoxWindow
        PictureBoxWindowInstance = New VisualBar

        g = PictureBoxWindowInstance.PictureBox_Main.CreateGraphics()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button_Connect.Click

        If Connected = True Then

            DisconnectFromDolphin()

            Exit Sub

        End If

        pipeClient = New NamedPipeClientStream(
                 ".", "DolphinMemoryTransfer",
                 PipeDirection.InOut, PipeOptions.Asynchronous,
                 TokenImpersonationLevel.Impersonation)

        Try

            pipeClient.Connect(300)

        Catch ex As TimeoutException

            MsgBox("No connection could be established!", MsgBoxStyle.Exclamation, "Warning")

            Exit Sub

        End Try

        Connected = True

        Button_Connect.Text = "Disconnect from Dolphin"

        ListBoxWindowInstance.Show()
        PictureBoxWindowInstance.Show()

        Timer_Update.Start()

    End Sub

    Private Sub Form_Closed(sender As Object, e As EventArgs) Handles Me.Closed

        If Connected = True Then

            DisconnectFromDolphin()

        End If

        ListBoxWindowInstance.Dispose()
        PictureBoxWindowInstance.Dispose()

    End Sub

    Private Sub DisconnectFromDolphin()

        ListBoxWindowInstance.Hide()
        PictureBoxWindowInstance.Hide()

        Timer_Update.Stop()
        pipeClient.Close()

        Connected = False

        Button_Connect.Text = "Connect to Dolphin"

    End Sub

    Private Sub Timer_Update_Tick(sender As Object, e As EventArgs) Handles Timer_Update.Tick

        Timer_Update.Stop()

        If (pipeClient.IsConnected = False) Then

            DisconnectFromDolphin()

            Exit Sub

        End If

        'Send Request
        Dim request As New StreamString(pipeClient)

        If (request.WriteString("ACT Memory") = -1) Then

            DisconnectFromDolphin()
            Exit Sub

        End If

        Try

            Dim returnedCommand As Byte = pipeClient.ReadByte()

            If (returnedCommand = 2) Then

                Dim Blue As Brush = Brushes.DodgerBlue
                Dim Red As Brush = Brushes.OrangeRed
                Dim Purple As Brush = Brushes.Purple
                Dim Green As Brush = Brushes.Green
                Dim Yellow As Brush = Brushes.Yellow
                Dim Black As Pen = New Pen(Color.Black, 3)

                Dim topItemIndex As Integer = 0
                Dim currentSelectedIndex As Integer = -1

                Try

                    topItemIndex = ListBoxWindowInstance.ListView1.TopItem.Index
                    currentSelectedIndex = ListBoxWindowInstance.ListView1.SelectedItems(0).Index

                Catch ex As Exception

                End Try

                ListBoxWindowInstance.ListView1.BeginUpdate()
                ListBoxWindowInstance.ListView1.Items.Clear()

                g.Clear(Color.White)

                Dim numElements As Byte()
                Dim countElements As Integer
                Dim bufferSize As UInt32
                Dim streamBuffer As Byte()

                numElements = New Byte(2) {}
                pipeClient.Read(numElements, 0, 2)

                countElements = Read16(numElements, 0)

                bufferSize = countElements * 12 '12 bytes per index

                streamBuffer = New Byte(bufferSize) {}
                pipeClient.Read(streamBuffer, 0, bufferSize) 'Read data into buffer

                Dim currentPos As UInt32 = 0
                Dim FirstAddress As UInt32 = 0

                Dim startAddress As UInt32
                Dim endAddress As UInt32
                Dim sizeSlot As UInt32

                For n As Integer = 1 To countElements

                    startAddress = Read32(streamBuffer, currentPos)
                    endAddress = Read32(streamBuffer, currentPos + 4)
                    sizeSlot = Read32(streamBuffer, currentPos + 8)

                    Dim str(3) As String
                    Dim item As ListViewItem

                    Dim sizeKB As Double = sizeSlot / 1000

                    str(0) = startAddress.ToString("X8")
                    str(1) = endAddress.ToString("X8")
                    str(2) = sizeKB.ToString.Replace(",", ".")

                    item = New ListViewItem(str)
                    ListBoxWindowInstance.ListView1.Items.Add(item)

                    currentPos = currentPos + 12

                Next

                FirstAddress = startAddress

                'Update DrawBox
                currentPos = 0
                Dim SmallSlotsTotalSize As Integer = 0
                Dim LastSmallSlotWasUsed As Boolean = False
                Dim SmallSlotsMode As Boolean = False

                Dim SearchSlotAddress As Integer = 0
                Dim SearchSlotSize As Integer = 0
                Dim SearchSlotSuccess As Boolean = False
                Dim SearchSlotRealAddress As UInt32 = 0

                For n As Integer = 1 To countElements

                    Dim IsUsed As Boolean = False

                    startAddress = Read32(streamBuffer, currentPos)
                    endAddress = Read32(streamBuffer, currentPos + 4)
                    sizeSlot = endAddress - startAddress

                    If (Read32(streamBuffer, currentPos + 8) > 0) Then

                        IsUsed = False
                        sizeSlot = Read32(streamBuffer, currentPos + 8)

                    Else

                        IsUsed = True
                        sizeSlot = endAddress - startAddress

                    End If


                    'Search for Slot 
                    If (FindSlotSize <> -1) Then

                        If (IsUsed = False And sizeSlot / 1000 >= FindSlotSize) Then

                            SearchSlotSize = sizeSlot / 10000
                            SearchSlotAddress = (startAddress - FirstAddress) / 10000
                            SearchSlotRealAddress = startAddress

                            SearchSlotSuccess = True

                        End If

                    Else

                        SearchSlotSuccess = False

                    End If



                    If (sizeSlot < 10000) Then 'Ignore slots < 10 kb

                        SmallSlotsMode = True

                        SmallSlotsMode = False

                        currentPos = currentPos + 12

                        Continue For

                        If (LastSmallSlotWasUsed <> IsUsed) Then

                            SmallSlotsTotalSize = 0

                        End If

                        SmallSlotsTotalSize = SmallSlotsTotalSize + sizeSlot
                        LastSmallSlotWasUsed = IsUsed

                        If (SmallSlotsTotalSize < 10000) Then

                            currentPos = currentPos + 12

                            Continue For

                        Else

                            startAddress = endAddress - SmallSlotsTotalSize
                            sizeSlot = SmallSlotsTotalSize

                            SmallSlotsTotalSize = 0
                            SmallSlotsMode = False

                        End If

                    Else

                        If (SmallSlotsMode) Then

                            If (LastSmallSlotWasUsed = IsUsed) Then

                                SmallSlotsTotalSize = SmallSlotsTotalSize + sizeSlot

                                startAddress = endAddress - SmallSlotsTotalSize
                                sizeSlot = SmallSlotsTotalSize

                            End If

                            SmallSlotsTotalSize = 0
                            SmallSlotsMode = False

                        End If

                    End If


                    Dim rectanglePixelWidth As Integer = sizeSlot / 10000

                    Dim distanceFromStart As Integer = (startAddress - FirstAddress) / 10000

                    If (IsUsed) Then

                        g.FillRectangle(Red, distanceFromStart, 0, rectanglePixelWidth, 120)

                    Else

                        g.FillRectangle(Blue, distanceFromStart, 0, rectanglePixelWidth, 120)

                    End If

                    currentPos = currentPos + 12

                Next

                'Update ListView
                Dim s As Sorter = DirectCast(ListBoxWindowInstance.ListView1.ListViewItemSorter, Sorter)

                s.Column = CurrentSortingColumn
                s.Order = CurrentSortingOrder

                ListBoxWindowInstance.ListView1.Sort()

                ListBoxWindowInstance.ListView1.EndUpdate()

                Try

                    ListBoxWindowInstance.ListView1.TopItem = ListBoxWindowInstance.ListView1.Items(topItemIndex)

                    ListBoxWindowInstance.ListView1.Items(currentSelectedIndex).Selected = True

                Catch ex As Exception

                End Try

                ListBoxWindowInstance.TextBox_SlotAmount.Text = ListBoxWindowInstance.ListView1.Items.Count


                'Highlight next free slot if found and requested
                If (FindSlotSize <> -1) Then

                    If (SearchSlotSuccess = True) Then

                        g.FillRectangle(Green, SearchSlotAddress, 0, SearchSlotSize, 120)

                        PictureBoxWindowInstance.Label_Arrow.Text = "⇑"

                        PictureBoxWindowInstance.Label_Arrow.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                        PictureBoxWindowInstance.Label_Arrow.ForeColor = System.Drawing.Color.DarkGreen

                        Dim Offset As Integer = SearchSlotSize / 2

                        PictureBoxWindowInstance.Label_Arrow.Location = New System.Drawing.Point(PictureBoxWindowInstance.PictureBox_Main.Location.X + SearchSlotAddress + Offset - 18, 115)

                        PictureBoxWindowInstance.Label_Arrow.Visible = True

                        For Each Item As ListViewItem In ListBoxWindowInstance.ListView1.Items

                            If Item.Text = SearchSlotRealAddress.ToString("X8") Then

                                Item.Selected = True
                                ListBoxWindowInstance.ListView1.TopItem = Item

                                Exit For

                            End If

                        Next

                    Else

                        PictureBoxWindowInstance.Label_Arrow.Text = "No Result"

                        PictureBoxWindowInstance.Label_Arrow.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                        PictureBoxWindowInstance.Label_Arrow.ForeColor = System.Drawing.Color.Black
                        PictureBoxWindowInstance.Label_Arrow.Location = New System.Drawing.Point(501, 122)

                        PictureBoxWindowInstance.Label_Arrow.Visible = True

                    End If

                Else

                    PictureBoxWindowInstance.Label_Arrow.Visible = False

                End If



            End If

        Catch ex As IOException

            DisconnectFromDolphin()
            Exit Sub

        End Try

        Timer_Update.Start()

    End Sub

    Private Sub TextBox_FindSlot_TextChanged(sender As Object, e As EventArgs) Handles TextBox_FindSlot.TextChanged

        If TextBox_FindSlot.Text = Nothing Then

            FindSlotSize = -1
            PictureBoxWindowInstance.Label_Arrow.Visible = False

            Exit Sub

        End If

        If (IsNumeric(TextBox_FindSlot.Text)) Then

            FindSlotSize = TextBox_FindSlot.Text

            If (FindSlotSize < 10 Or FindSlotSize > 10000) Then

                FindSlotSize = -1
                PictureBoxWindowInstance.Label_Arrow.Visible = False

            End If

        Else

            FindSlotSize = -1
            PictureBoxWindowInstance.Label_Arrow.Visible = False

        End If

    End Sub

    Private Function Read8(Data() As Byte, Offset As Integer) As Byte

        Return Buffer.GetByte(Data, Offset)

    End Function

    Private Function Read16(Data() As Byte, Offset As Integer) As UInt16

        Dim Output(1) As Byte

        Output(1) = Buffer.GetByte(Data, Offset + 1)
        Output(0) = Buffer.GetByte(Data, Offset) << 8

        Return BitConverter.ToUInt16(Output, 0)

    End Function

    Private Function Read32(Data() As Byte, Offset As Integer) As UInt32

        Dim Output(3) As Byte

        Output(3) = Buffer.GetByte(Data, Offset + 3)
        Output(2) = Buffer.GetByte(Data, Offset + 2) << 8
        Output(1) = Buffer.GetByte(Data, Offset + 1) << 16
        Output(0) = Buffer.GetByte(Data, Offset) << 24

        Return BitConverter.ToUInt32(Output, 0)

    End Function

End Class


'Defines the data protocol for reading and writing strings on our stream
Public Class StreamString

    Private ioStream As Stream
    Private streamEncoding As ASCIIEncoding

    Public Sub New(ioStream As Stream)

        Me.ioStream = ioStream
        streamEncoding = New ASCIIEncoding()

    End Sub

    Public Function ReadString(length As Integer) As String

        Dim inBuffer As Array = Array.CreateInstance(GetType(Byte), length)

        Try

            ioStream.Read(inBuffer, 0, length)

        Catch ex As IOException

            Return ""

        End Try

        Return streamEncoding.GetString(inBuffer)

    End Function

    Public Function WriteString(outString As String) As Integer

        Dim outBuffer() As Byte = streamEncoding.GetBytes(outString)

        Try

            ioStream.Write(outBuffer, 0, outBuffer.Length)
            ioStream.Flush()

        Catch ex As IOException

            Return -1

        End Try

        Return outBuffer.Length

    End Function

End Class
