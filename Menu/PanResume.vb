Imports LiveCharts
Imports LiveCharts.Defaults
Imports LiveCharts.Wpf
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms


Public Class PanResume


    Public Sub PanResume_Load(sender As Object, e As EventArgs) Handles MyBase.Load




        Dim tot_iscritti_mese(12) As Integer
        Dim tot_progettazione_mese(12) As Integer
        Dim meseN1 As Integer = 0



        For i = 0 To num_lines - 1

            Dim len_data As Integer = matrix_storico(i, 0).Length
            Dim mese_line As String
            Dim anno_line As String


            If len_data = 6 Then

                mese_line = matrix_storico(i, 0).Substring(0, matrix_storico(i, 0).IndexOf("_"))
                anno_line = matrix_storico(i, 0).Substring(matrix_storico(i, 0).IndexOf("_") + 1, matrix_storico(i, 0).Length - 2)

            ElseIf len_data = 7 Then

                mese_line = matrix_storico(i, 0).Substring(0, matrix_storico(i, 0).IndexOf("_"))
                anno_line = matrix_storico(i, 0).Substring(matrix_storico(i, 0).IndexOf("_") + 1, matrix_storico(i, 0).Length - 3)

            End If


            tot_progettazione_mese(i) = 0
            If anno_line = Today.Year Then

                tot_iscritti_mese(i) = matrix_storico(i, 17)


                For o = 1 To 16

                    If o = 1 Or o = 2 Or o = 3 Or o = 9 Or o = 10 Or o = 11 Then
                        tot_progettazione_mese(i) = tot_progettazione_mese(i) + matrix_storico(i, o)
                    End If

                Next


            End If

            If i = num_lines - 2 Then

                meseN1 = tot_progettazione_mese(i)

            End If


        Next


        Label7.Text = tot_projects
        Label1.Text = "+" & matrix_storico(num_lines - 1, 17) - matrix_storico(num_lines - 2, 17) 'numero di iscritti in + questo mese
        Label4.Text = "+" & tot_progettazione_mese(num_lines - 1) - meseN1 'numero di iscritti in + questo mese


        If Today.Month = 1 Then
            Label6.Text = "Gennaio " & Today.Year
        ElseIf Today.Month = 2 Then
            Label6.Text = "Febbraio " & Today.Year
        ElseIf Today.Month = 3 Then
            Label6.Text = "Marzo " & Today.Year
        ElseIf Today.Month = 4 Then
            Label6.Text = "Aprile " & Today.Year
        ElseIf Today.Month = 5 Then
            Label6.Text = "Maggio " & Today.Year
        ElseIf Today.Month = 6 Then
            Label6.Text = "Giugno " & Today.Year
        ElseIf Today.Month = 7 Then
            Label6.Text = "Luglio " & Today.Year
        ElseIf Today.Month = 8 Then
            Label6.Text = "Agosto " & Today.Year
        ElseIf Today.Month = 9 Then
            Label6.Text = "Settembre " & Today.Year
        ElseIf Today.Month = 10 Then
            Label6.Text = "Ottobre " & Today.Year
        ElseIf Today.Month = 11 Then
            Label6.Text = "Novembre " & Today.Year
        ElseIf Today.Month = 12 Then
            Label6.Text = "Dicembre " & Today.Year
        End If


        CartesianChart1.AxisX.Add(New Axis With {.Title = "Month", .Labels = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Opt", "Nov", "Dec"}, .MinValue = 0})
        CartesianChart1.AxisY.Add(New Axis With {.Title = "Progettazioni", .MinValue = 0})


        CartesianChart2.AxisX.Add(New Axis With {.Title = "Month", .Labels = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Opt", "Nov", "Dec"}})
        CartesianChart2.AxisY.Add(New Axis With {.Title = "Users", .MinValue = 0})

        'CartesianChart2.AxisY.Add(New Axis With {.Title = "Subscribed", .LabelFormatter = Function(value) value.ToString, .MinValue = 0})

        'gennaio
        If Today.Month = 1 Then
            CartesianChart1.Series = New SeriesCollection From {
                New ColumnSeries With {
               .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0))
               },
                    .PointGeometrySize = 15
                }
         }
        End If

        'febbraio
        If Today.Month = 2 Then
            CartesianChart1.Series = New SeriesCollection From {
                New ColumnSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0)),
                        New ObservablePoint(1, tot_progettazione_mese(1))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0)),
                        New ObservablePoint(1, tot_progettazione_mese(1))
               },
                    .PointGeometrySize = 15
                }
         }
        End If

        'marzo
        If Today.Month = 3 Then
            CartesianChart1.Series = New SeriesCollection From {
                New ColumnSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0)),
                        New ObservablePoint(1, tot_progettazione_mese(1)),
                        New ObservablePoint(2, tot_progettazione_mese(2))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0)),
                        New ObservablePoint(1, tot_progettazione_mese(1)),
                        New ObservablePoint(2, tot_progettazione_mese(2))
               },
                    .PointGeometrySize = 15
                }
         }
        End If

        'aprile
        If Today.Month = 4 Then
            CartesianChart1.Series = New SeriesCollection From {
                New ColumnSeries With {
            .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0)),
                        New ObservablePoint(1, tot_progettazione_mese(1)),
                        New ObservablePoint(2, tot_progettazione_mese(2)),
                        New ObservablePoint(3, tot_progettazione_mese(3))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0)),
                        New ObservablePoint(1, tot_progettazione_mese(1)),
                        New ObservablePoint(2, tot_progettazione_mese(2)),
                        New ObservablePoint(3, tot_progettazione_mese(3))
               },
                    .PointGeometrySize = 15
                }
         }
        End If

        'maggio
        If Today.Month = 5 Then
            CartesianChart1.Series = New SeriesCollection From {
                New ColumnSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0)),
                        New ObservablePoint(1, tot_progettazione_mese(1)),
                        New ObservablePoint(2, tot_progettazione_mese(2)),
                        New ObservablePoint(3, tot_progettazione_mese(3)),
                        New ObservablePoint(4, tot_progettazione_mese(4))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0)),
                        New ObservablePoint(1, tot_progettazione_mese(1)),
                        New ObservablePoint(2, tot_progettazione_mese(2)),
                        New ObservablePoint(3, tot_progettazione_mese(3)),
                        New ObservablePoint(4, tot_progettazione_mese(4))
               },
                    .PointGeometrySize = 15
                }
         }
        End If

        'giugno
        If Today.Month = 6 Then
            CartesianChart1.Series = New SeriesCollection From {
                New ColumnSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0)),
                        New ObservablePoint(1, tot_progettazione_mese(1)),
                        New ObservablePoint(2, tot_progettazione_mese(2)),
                        New ObservablePoint(3, tot_progettazione_mese(3)),
                        New ObservablePoint(4, tot_progettazione_mese(4)),
                        New ObservablePoint(5, tot_progettazione_mese(5))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0)),
                        New ObservablePoint(1, tot_progettazione_mese(1)),
                        New ObservablePoint(2, tot_progettazione_mese(2)),
                        New ObservablePoint(3, tot_progettazione_mese(3)),
                        New ObservablePoint(4, tot_progettazione_mese(4)),
                        New ObservablePoint(5, tot_progettazione_mese(5))
               },
                    .PointGeometrySize = 15
                }
         }
        End If

        'luglio
        If Today.Month = 7 Then
            CartesianChart1.Series = New SeriesCollection From {
                New ColumnSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0)),
                        New ObservablePoint(1, tot_progettazione_mese(1)),
                        New ObservablePoint(2, tot_progettazione_mese(2)),
                        New ObservablePoint(3, tot_progettazione_mese(3)),
                        New ObservablePoint(4, tot_progettazione_mese(4)),
                        New ObservablePoint(5, tot_progettazione_mese(5)),
                        New ObservablePoint(6, tot_progettazione_mese(6))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0)),
                        New ObservablePoint(1, tot_progettazione_mese(1)),
                        New ObservablePoint(2, tot_progettazione_mese(2)),
                        New ObservablePoint(3, tot_progettazione_mese(3)),
                        New ObservablePoint(4, tot_progettazione_mese(4)),
                        New ObservablePoint(5, tot_progettazione_mese(5)),
                        New ObservablePoint(6, tot_progettazione_mese(6))
               },
                    .PointGeometrySize = 15
                }
         }
        End If

        'agosto
        If Today.Month = 8 Then
            CartesianChart1.Series = New SeriesCollection From {
                New ColumnSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0)),
                        New ObservablePoint(1, tot_progettazione_mese(1)),
                        New ObservablePoint(2, tot_progettazione_mese(2)),
                        New ObservablePoint(3, tot_progettazione_mese(3)),
                        New ObservablePoint(4, tot_progettazione_mese(4)),
                        New ObservablePoint(5, tot_progettazione_mese(5)),
                        New ObservablePoint(6, tot_progettazione_mese(6)),
                        New ObservablePoint(7, tot_progettazione_mese(7))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0)),
                        New ObservablePoint(1, tot_progettazione_mese(1)),
                        New ObservablePoint(2, tot_progettazione_mese(2)),
                        New ObservablePoint(3, tot_progettazione_mese(3)),
                        New ObservablePoint(4, tot_progettazione_mese(4)),
                        New ObservablePoint(5, tot_progettazione_mese(5)),
                        New ObservablePoint(6, tot_progettazione_mese(6)),
                        New ObservablePoint(7, tot_progettazione_mese(7))
               },
                    .PointGeometrySize = 15
                }
         }
        End If

        'settembre
        If Today.Month = 9 Then
            CartesianChart1.Series = New SeriesCollection From {
                New ColumnSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0)),
                        New ObservablePoint(1, tot_progettazione_mese(1)),
                        New ObservablePoint(2, tot_progettazione_mese(2)),
                        New ObservablePoint(3, tot_progettazione_mese(3)),
                        New ObservablePoint(4, tot_progettazione_mese(4)),
                        New ObservablePoint(5, tot_progettazione_mese(5)),
                        New ObservablePoint(6, tot_progettazione_mese(6)),
                        New ObservablePoint(7, tot_progettazione_mese(7)),
                        New ObservablePoint(8, tot_progettazione_mese(8))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0)),
                        New ObservablePoint(1, tot_progettazione_mese(1)),
                        New ObservablePoint(2, tot_progettazione_mese(2)),
                        New ObservablePoint(3, tot_progettazione_mese(3)),
                        New ObservablePoint(4, tot_progettazione_mese(4)),
                        New ObservablePoint(5, tot_progettazione_mese(5)),
                        New ObservablePoint(6, tot_progettazione_mese(6)),
                        New ObservablePoint(7, tot_progettazione_mese(7)),
                        New ObservablePoint(8, tot_progettazione_mese(8))
               },
                    .PointGeometrySize = 15
                }
         }
        End If


        'ottobre
        If Today.Month = 10 Then
            CartesianChart1.Series = New SeriesCollection From {
                New ColumnSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0)),
                        New ObservablePoint(1, tot_progettazione_mese(1)),
                        New ObservablePoint(2, tot_progettazione_mese(2)),
                        New ObservablePoint(3, tot_progettazione_mese(3)),
                        New ObservablePoint(4, tot_progettazione_mese(4)),
                        New ObservablePoint(5, tot_progettazione_mese(5)),
                        New ObservablePoint(6, tot_progettazione_mese(6)),
                        New ObservablePoint(7, tot_progettazione_mese(7)),
                        New ObservablePoint(8, tot_progettazione_mese(8)),
                        New ObservablePoint(9, tot_progettazione_mese(9))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0)),
                        New ObservablePoint(1, tot_progettazione_mese(1)),
                        New ObservablePoint(2, tot_progettazione_mese(2)),
                        New ObservablePoint(3, tot_progettazione_mese(3)),
                        New ObservablePoint(4, tot_progettazione_mese(4)),
                        New ObservablePoint(5, tot_progettazione_mese(5)),
                        New ObservablePoint(6, tot_progettazione_mese(6)),
                        New ObservablePoint(7, tot_progettazione_mese(7)),
                        New ObservablePoint(8, tot_progettazione_mese(8)),
                        New ObservablePoint(9, tot_progettazione_mese(9))
               },
                    .PointGeometrySize = 15
                }
         }
        End If

        'novembre
        If Today.Month = 11 Then
            CartesianChart1.Series = New SeriesCollection From {
                New ColumnSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0)),
                        New ObservablePoint(1, tot_progettazione_mese(1)),
                        New ObservablePoint(2, tot_progettazione_mese(2)),
                        New ObservablePoint(3, tot_progettazione_mese(3)),
                        New ObservablePoint(4, tot_progettazione_mese(4)),
                        New ObservablePoint(5, tot_progettazione_mese(5)),
                        New ObservablePoint(6, tot_progettazione_mese(6)),
                        New ObservablePoint(7, tot_progettazione_mese(7)),
                        New ObservablePoint(8, tot_progettazione_mese(8)),
                        New ObservablePoint(9, tot_progettazione_mese(9)),
                        New ObservablePoint(10, tot_progettazione_mese(10))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0)),
                        New ObservablePoint(1, tot_progettazione_mese(1)),
                        New ObservablePoint(2, tot_progettazione_mese(2)),
                        New ObservablePoint(3, tot_progettazione_mese(3)),
                        New ObservablePoint(4, tot_progettazione_mese(4)),
                        New ObservablePoint(5, tot_progettazione_mese(5)),
                        New ObservablePoint(6, tot_progettazione_mese(6)),
                        New ObservablePoint(7, tot_progettazione_mese(7)),
                        New ObservablePoint(8, tot_progettazione_mese(8)),
                        New ObservablePoint(9, tot_progettazione_mese(9)),
                        New ObservablePoint(10, tot_progettazione_mese(10))
               },
                    .PointGeometrySize = 15
                }
         }
        End If



        'dicembre
        If Today.Month = 12 Then
            CartesianChart1.Series = New SeriesCollection From {
                New ColumnSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0)),
                        New ObservablePoint(1, tot_progettazione_mese(1)),
                        New ObservablePoint(2, tot_progettazione_mese(2)),
                        New ObservablePoint(3, tot_progettazione_mese(3)),
                        New ObservablePoint(4, tot_progettazione_mese(4)),
                        New ObservablePoint(5, tot_progettazione_mese(5)),
                        New ObservablePoint(6, tot_progettazione_mese(6)),
                        New ObservablePoint(7, tot_progettazione_mese(7)),
                        New ObservablePoint(8, tot_progettazione_mese(8)),
                        New ObservablePoint(9, tot_progettazione_mese(9)),
                        New ObservablePoint(10, tot_progettazione_mese(10)),
                        New ObservablePoint(11, tot_progettazione_mese(11))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_progettazione_mese(0)),
                        New ObservablePoint(1, tot_progettazione_mese(1)),
                        New ObservablePoint(2, tot_progettazione_mese(2)),
                        New ObservablePoint(3, tot_progettazione_mese(3)),
                        New ObservablePoint(4, tot_progettazione_mese(4)),
                        New ObservablePoint(5, tot_progettazione_mese(5)),
                        New ObservablePoint(6, tot_progettazione_mese(6)),
                        New ObservablePoint(7, tot_progettazione_mese(7)),
                        New ObservablePoint(8, tot_progettazione_mese(8)),
                        New ObservablePoint(9, tot_progettazione_mese(9)),
                        New ObservablePoint(10, tot_progettazione_mese(10)),
                        New ObservablePoint(11, tot_progettazione_mese(11))
                    },
                    .PointGeometrySize = 15
                }
}
        End If






        'gennaio
        If Today.Month = 1 Then
            CartesianChart2.Series = New SeriesCollection From {
                New ColumnSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0))
               },
                    .PointGeometrySize = 15
                }
         }
        End If

        'febbraio
        If Today.Month = 2 Then
            CartesianChart2.Series = New SeriesCollection From {
                New ColumnSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0)),
                        New ObservablePoint(1, tot_iscritti_mese(1))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0)),
                        New ObservablePoint(1, tot_iscritti_mese(1)),
                        New ObservablePoint(2, tot_iscritti_mese(2))
               },
                    .PointGeometrySize = 15
                }
         }
        End If

        'marzo
        If Today.Month = 3 Then
            CartesianChart2.Series = New SeriesCollection From {
                New ColumnSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0)),
                        New ObservablePoint(1, tot_iscritti_mese(1)),
                        New ObservablePoint(2, tot_iscritti_mese(2))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0)),
                        New ObservablePoint(1, tot_iscritti_mese(1)),
                        New ObservablePoint(2, tot_iscritti_mese(2))
               },
                    .PointGeometrySize = 15
                }
         }
        End If

        'aprile
        If Today.Month = 4 Then
            CartesianChart2.Series = New SeriesCollection From {
                New ColumnSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0)),
                        New ObservablePoint(1, tot_iscritti_mese(1)),
                        New ObservablePoint(2, tot_iscritti_mese(2)),
                        New ObservablePoint(3, tot_iscritti_mese(3))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0)),
                        New ObservablePoint(1, tot_iscritti_mese(1)),
                        New ObservablePoint(2, tot_iscritti_mese(2)),
                        New ObservablePoint(3, tot_iscritti_mese(3))
               },
                    .PointGeometrySize = 15
                }
         }
        End If

        'maggio
        If Today.Month = 5 Then
            CartesianChart2.Series = New SeriesCollection From {
                New ColumnSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0)),
                        New ObservablePoint(1, tot_iscritti_mese(1)),
                        New ObservablePoint(2, tot_iscritti_mese(2)),
                        New ObservablePoint(3, tot_iscritti_mese(3)),
                        New ObservablePoint(4, tot_iscritti_mese(4))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0)),
                        New ObservablePoint(1, tot_iscritti_mese(1)),
                        New ObservablePoint(2, tot_iscritti_mese(2)),
                        New ObservablePoint(3, tot_iscritti_mese(3)),
                        New ObservablePoint(4, tot_iscritti_mese(4))
               },
                    .PointGeometrySize = 15
                }
         }
        End If

        'giugno
        If Today.Month = 6 Then
            CartesianChart2.Series = New SeriesCollection From {
                New ColumnSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0)),
                        New ObservablePoint(1, tot_iscritti_mese(1)),
                        New ObservablePoint(2, tot_iscritti_mese(2)),
                        New ObservablePoint(3, tot_iscritti_mese(3)),
                        New ObservablePoint(4, tot_iscritti_mese(4)),
                        New ObservablePoint(5, tot_iscritti_mese(5))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0)),
                        New ObservablePoint(1, tot_iscritti_mese(1)),
                        New ObservablePoint(2, tot_iscritti_mese(2)),
                        New ObservablePoint(3, tot_iscritti_mese(3)),
                        New ObservablePoint(4, tot_iscritti_mese(4)),
                        New ObservablePoint(5, tot_iscritti_mese(5))
               },
                    .PointGeometrySize = 15
                }
         }
        End If

        'luglio
        If Today.Month = 7 Then
            CartesianChart2.Series = New SeriesCollection From {
                New ColumnSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0)),
                        New ObservablePoint(1, tot_iscritti_mese(1)),
                        New ObservablePoint(2, tot_iscritti_mese(2)),
                        New ObservablePoint(3, tot_iscritti_mese(3)),
                        New ObservablePoint(4, tot_iscritti_mese(4)),
                        New ObservablePoint(5, tot_iscritti_mese(5)),
                        New ObservablePoint(6, tot_iscritti_mese(6))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0)),
                        New ObservablePoint(1, tot_iscritti_mese(1)),
                        New ObservablePoint(2, tot_iscritti_mese(2)),
                        New ObservablePoint(3, tot_iscritti_mese(3)),
                        New ObservablePoint(4, tot_iscritti_mese(4)),
                        New ObservablePoint(5, tot_iscritti_mese(5)),
                        New ObservablePoint(6, tot_iscritti_mese(6))
               },
                    .PointGeometrySize = 15
                }
         }
        End If

        'agosto
        If Today.Month = 8 Then
            CartesianChart2.Series = New SeriesCollection From {
                New ColumnSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0)),
                        New ObservablePoint(1, tot_iscritti_mese(1)),
                        New ObservablePoint(2, tot_iscritti_mese(2)),
                        New ObservablePoint(3, tot_iscritti_mese(3)),
                        New ObservablePoint(4, tot_iscritti_mese(4)),
                        New ObservablePoint(5, tot_iscritti_mese(5)),
                        New ObservablePoint(6, tot_iscritti_mese(6)),
                        New ObservablePoint(7, tot_iscritti_mese(7))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0)),
                        New ObservablePoint(1, tot_iscritti_mese(1)),
                        New ObservablePoint(2, tot_iscritti_mese(2)),
                        New ObservablePoint(3, tot_iscritti_mese(3)),
                        New ObservablePoint(4, tot_iscritti_mese(4)),
                        New ObservablePoint(5, tot_iscritti_mese(5)),
                        New ObservablePoint(6, tot_iscritti_mese(6)),
                        New ObservablePoint(7, tot_iscritti_mese(7))
               },
                    .PointGeometrySize = 15
                }
         }
        End If

        'settembre
        If Today.Month = 9 Then
            CartesianChart2.Series = New SeriesCollection From {
                New ColumnSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0)),
                        New ObservablePoint(1, tot_iscritti_mese(1)),
                        New ObservablePoint(2, tot_iscritti_mese(2)),
                        New ObservablePoint(3, tot_iscritti_mese(3)),
                        New ObservablePoint(4, tot_iscritti_mese(4)),
                        New ObservablePoint(5, tot_iscritti_mese(5)),
                        New ObservablePoint(6, tot_iscritti_mese(6)),
                        New ObservablePoint(7, tot_iscritti_mese(7)),
                        New ObservablePoint(8, tot_iscritti_mese(8))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0)),
                        New ObservablePoint(1, tot_iscritti_mese(1)),
                        New ObservablePoint(2, tot_iscritti_mese(2)),
                        New ObservablePoint(3, tot_iscritti_mese(3)),
                        New ObservablePoint(4, tot_iscritti_mese(4)),
                        New ObservablePoint(5, tot_iscritti_mese(5)),
                        New ObservablePoint(6, tot_iscritti_mese(6)),
                        New ObservablePoint(7, tot_iscritti_mese(7)),
                        New ObservablePoint(8, tot_iscritti_mese(8))
               },
                    .PointGeometrySize = 15
                }
         }
        End If


        'ottobre
        If Today.Month = 10 Then
            CartesianChart2.Series = New SeriesCollection From {
                New ColumnSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0)),
                        New ObservablePoint(1, tot_iscritti_mese(1)),
                        New ObservablePoint(2, tot_iscritti_mese(2)),
                        New ObservablePoint(3, tot_iscritti_mese(3)),
                        New ObservablePoint(4, tot_iscritti_mese(4)),
                        New ObservablePoint(5, tot_iscritti_mese(5)),
                        New ObservablePoint(6, tot_iscritti_mese(6)),
                        New ObservablePoint(7, tot_iscritti_mese(7)),
                        New ObservablePoint(8, tot_iscritti_mese(8)),
                        New ObservablePoint(9, tot_iscritti_mese(9))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0)),
                        New ObservablePoint(1, tot_iscritti_mese(1)),
                        New ObservablePoint(2, tot_iscritti_mese(2)),
                        New ObservablePoint(3, tot_iscritti_mese(3)),
                        New ObservablePoint(4, tot_iscritti_mese(4)),
                        New ObservablePoint(5, tot_iscritti_mese(5)),
                        New ObservablePoint(6, tot_iscritti_mese(6)),
                        New ObservablePoint(7, tot_iscritti_mese(7)),
                        New ObservablePoint(8, tot_iscritti_mese(8)),
                        New ObservablePoint(9, tot_iscritti_mese(9))
               },
                    .PointGeometrySize = 15
                }
         }
        End If

        'novembre
        If Today.Month = 11 Then
            CartesianChart2.Series = New SeriesCollection From {
                New ColumnSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0)),
                        New ObservablePoint(1, tot_iscritti_mese(1)),
                        New ObservablePoint(2, tot_iscritti_mese(2)),
                        New ObservablePoint(3, tot_iscritti_mese(3)),
                        New ObservablePoint(4, tot_iscritti_mese(4)),
                        New ObservablePoint(5, tot_iscritti_mese(5)),
                        New ObservablePoint(6, tot_iscritti_mese(6)),
                        New ObservablePoint(7, tot_iscritti_mese(7)),
                        New ObservablePoint(8, tot_iscritti_mese(8)),
                        New ObservablePoint(9, tot_iscritti_mese(9)),
                        New ObservablePoint(10, tot_iscritti_mese(10))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0)),
                        New ObservablePoint(1, tot_iscritti_mese(1)),
                        New ObservablePoint(2, tot_iscritti_mese(2)),
                        New ObservablePoint(3, tot_iscritti_mese(3)),
                        New ObservablePoint(4, tot_iscritti_mese(4)),
                        New ObservablePoint(5, tot_iscritti_mese(5)),
                        New ObservablePoint(6, tot_iscritti_mese(6)),
                        New ObservablePoint(7, tot_iscritti_mese(7)),
                        New ObservablePoint(8, tot_iscritti_mese(8)),
                        New ObservablePoint(9, tot_iscritti_mese(9)),
                        New ObservablePoint(10, tot_iscritti_mese(10))
               },
                    .PointGeometrySize = 15
                }
         }
        End If



        'dicembre
        If Today.Month = 12 Then
            CartesianChart2.Series = New SeriesCollection From {
                New ColumnSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0)),
                        New ObservablePoint(1, tot_iscritti_mese(1)),
                        New ObservablePoint(2, tot_iscritti_mese(2)),
                        New ObservablePoint(3, tot_iscritti_mese(3)),
                        New ObservablePoint(4, tot_iscritti_mese(4)),
                        New ObservablePoint(5, tot_iscritti_mese(5)),
                        New ObservablePoint(6, tot_iscritti_mese(6)),
                        New ObservablePoint(7, tot_iscritti_mese(7)),
                        New ObservablePoint(8, tot_iscritti_mese(8)),
                        New ObservablePoint(9, tot_iscritti_mese(9)),
                        New ObservablePoint(10, tot_iscritti_mese(10)),
                        New ObservablePoint(11, tot_iscritti_mese(11))
                    }
                },
                New LineSeries With {
                    .Values = New ChartValues(Of ObservablePoint) From {
                        New ObservablePoint(0, tot_iscritti_mese(0)),
                        New ObservablePoint(1, tot_iscritti_mese(1)),
                        New ObservablePoint(2, tot_iscritti_mese(2)),
                        New ObservablePoint(3, tot_iscritti_mese(3)),
                        New ObservablePoint(4, tot_iscritti_mese(4)),
                        New ObservablePoint(5, tot_iscritti_mese(5)),
                        New ObservablePoint(6, tot_iscritti_mese(6)),
                        New ObservablePoint(7, tot_iscritti_mese(7)),
                        New ObservablePoint(8, tot_iscritti_mese(8)),
                        New ObservablePoint(9, tot_iscritti_mese(9)),
                        New ObservablePoint(10, tot_iscritti_mese(10)),
                        New ObservablePoint(11, tot_iscritti_mese(11))
                    },
                    .PointGeometrySize = 15
                }
}
        End If




        'Grafico a torta progettazioni clienti
        Dim piechartData As SeriesCollection = New SeriesCollection From {
        New PieSeries With {
        .Title = "Duct Fan",
        .Values = New ChartValues(Of Double) From {
            tot_projects_DF_VD_TOT
        },
        .DataLabels = True
        },
        New PieSeries With {
        .Title = "Plug Fan",
        .Values = New ChartValues(Of Double) From {
            tot_projects_PF_VD_TOT
        },
        .DataLabels = True
        },
        New PieSeries With {
        .Title = "Panel Fan",
        .Values = New ChartValues(Of Double) From {
            tot_projects_PN_VD_TOT
            },
        .DataLabels = True
        }
        }




        'Grafico a torta progettazioni clienti
        Dim piechartData1 As SeriesCollection = New SeriesCollection From {
        New PieSeries With {
        .Title = "Safe Area",
        .Values = New ChartValues(Of Double) From {
            tot_projects_SF_VD_TOT
        },
        .DataLabels = True
        },
        New PieSeries With {
        .Title = "Atex",
        .Values = New ChartValues(Of Double) From {
            tot_projects_ATX_TOT
        },
        .DataLabels = True
        }
        }



        'Grafico a torta progettazioni clienti
        Dim piechartData2 As SeriesCollection = New SeriesCollection From {
        New PieSeries With {
        .Title = "Industrial",
        .Values = New ChartValues(Of Double) From {
            tot_projects_IND_TOT
        },
        .DataLabels = True
        },
        New PieSeries With {
        .Title = "Off Shore",
        .Values = New ChartValues(Of Double) From {
            tot_projects_OFF_TOT
        },
        .DataLabels = True
        },
        New PieSeries With {
        .Title = "Sea Shore",
        .Values = New ChartValues(Of Double) From {
            tot_projects_SEA_TOT
        },
        .DataLabels = True
        }
        }





        PieChart1.InnerRadius = 150
        PieChart1.HoverPushOut = 20
        PieChart2.InnerRadius = 60
        PieChart2.HoverPushOut = 12
        PieChart3.InnerRadius = 60
        PieChart3.HoverPushOut = 12



        PieChart1.Series = piechartData
        PieChart2.Series = piechartData1
        PieChart3.Series = piechartData2








    End Sub


End Class