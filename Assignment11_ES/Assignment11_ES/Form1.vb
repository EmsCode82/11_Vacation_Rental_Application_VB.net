Public Class Form1
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Close the Program

        Close()

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        'Clear all labels and textboxes reset focus

        txtFirstName.Clear()
        txtLastName.Clear()
        txtPhoneNumber.Clear()
        txtEmail.Clear()
        txtDays.Clear()
        lblSubTotal.ResetText()
        lblTax.ResetText()
        lblTotal.ResetText()
        radStandard.Checked = True
        radAAA.Checked = False
        radAARP.Checked = False
        cboState.ResetText()
        txtFirstName.Focus()

    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        'Declare Variables

        Dim intDays As Integer
        Dim dblDiscountTotal As Double

        Dim dblSubTotal As Double
        Dim dblTax As Double
        Dim dblTotal As Double

        'Get and Validate Inputs

        If txtFirstName.Text = String.Empty Then
            MessageBox.Show("First Name Must be Entered")
            txtFirstName.Focus()
            Exit Sub
        End If

        If txtLastName.Text = String.Empty Then
            MessageBox.Show("Last Name Must be Entered")
            txtLastName.Focus()
            Exit Sub
        End If

        If txtPhoneNumber.Text = String.Empty Then
            MessageBox.Show("Phone Number Must be Entered")
            txtPhoneNumber.Focus()
            Exit Sub
        End If

        If txtEmail.Text = String.Empty Then
            MessageBox.Show("Email Must be Entered")
            txtEmail.Focus()
            Exit Sub
        End If

        If IsNumeric(txtDays.Text) Then
            intDays = txtDays.Text
            If (intDays > 0) And (intDays < 60) Then
            Else
                MessageBox.Show("Number of Days Must be greater than 0 and less than 60")
                txtDays.Focus()
                Exit Sub
            End If
        Else
            MessageBox.Show("Number of Days Must be entered, and Must be Numeric")
            txtDays.Focus()
            Exit Sub
        End If

        If radOff.Checked Or radPeak.Checked Or radStandard.Checked Then
        Else
            MessageBox.Show("Season Must be Selected")
            radStandard.Focus()
            Exit Sub
        End If

        If cboState.Text = String.Empty Then
            MessageBox.Show("State Must be Selected")
            cboState.Focus()
            Exit Sub
        End If

        'Do Calculations

        If radOff.Checked Then
            dblSubTotal = (50 * intDays)
        Else
            If radPeak.Checked Then
                dblSubTotal = (150 * intDays)
            Else
                dblSubTotal = (100 * intDays)
            End If
        End If

        If radAAA.Checked Or radAARP.Checked Then
            dblDiscountTotal = dblSubTotal
            dblSubTotal = dblSubTotal - dblSubTotal * 0.025
            dblDiscountTotal = dblDiscountTotal - dblSubTotal
        End If

        If intDays > 30 Then
            dblDiscountTotal = dblDiscountTotal + dblSubTotal
            dblSubTotal = dblSubTotal * 0.9
            dblDiscountTotal = dblDiscountTotal - dblSubTotal
        Else
            If intDays > 14 Then
                dblDiscountTotal = dblDiscountTotal + dblSubTotal
                dblSubTotal = dblSubTotal * 0.95
                dblDiscountTotal = dblDiscountTotal - dblSubTotal
            End If
        End If

        If chkRepeat.Checked Then
            If dblSubTotal > 300 Then
                dblDiscountTotal = dblDiscountTotal + dblSubTotal
                dblSubTotal = dblSubTotal - 30
                dblDiscountTotal = dblDiscountTotal - dblSubTotal
            Else
                If dblSubTotal > 200 Then
                    dblDiscountTotal = dblDiscountTotal + dblSubTotal
                    dblSubTotal = dblSubTotal - 20
                    dblDiscountTotal = dblDiscountTotal - dblSubTotal
                Else
                    If dblSubTotal > 50 Then
                        dblDiscountTotal = dblDiscountTotal + dblSubTotal
                        dblSubTotal = dblSubTotal - 5
                        dblDiscountTotal = dblDiscountTotal - dblSubTotal
                    End If
                End If
            End If
        End If

        If radOff.Checked Then
            If dblDiscountTotal > 300 Then
                MessageBox.Show(FormatCurrency(dblDiscountTotal), "Your Discount")
                MessageBox.Show("The maximum discount for the Off Season is $300, The Maximum Discount will be applied.")
                dblSubTotal = dblSubTotal + dblDiscountTotal - 300
            End If
        End If

        If radStandard.Checked Then
            If dblDiscountTotal > 400 Then
                MessageBox.Show(FormatCurrency(dblDiscountTotal), "Your Discount")
                MessageBox.Show("The maximum discount for the Standard Season is $400, The Maximum Discount will be applied.")
                dblSubTotal = dblSubTotal + dblDiscountTotal - 400
            End If
        End If

        If radPeak.Checked Then
            If dblDiscountTotal > 500 Then
                MessageBox.Show(FormatCurrency(dblDiscountTotal), "Your Discount")
                MessageBox.Show("The maximum discount for the Peak Season is $500, The Maximum Discount will be applied.")
                dblSubTotal = dblSubTotal + dblDiscountTotal - 500
            End If
        End If

        If cboState.Text = "Florida" Then
            dblTax = 0
        Else
            dblTax = dblSubTotal * 0.1
        End If

        dblTotal = dblSubTotal + dblTax

        'Display Outputs

        lblSubTotal.Text = FormatCurrency(dblSubTotal)
        lblTax.Text = FormatCurrency(dblTax)
        lblTotal.Text = FormatCurrency(dblTotal)

    End Sub
End Class
