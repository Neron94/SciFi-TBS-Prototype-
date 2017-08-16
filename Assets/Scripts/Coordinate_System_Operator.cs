using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinate_System_Operator : MonoBehaviour {


    List<Square_cell_Operator>Open_List = new List<Square_cell_Operator>();
    List<Square_cell_Operator>Close_List = new List<Square_cell_Operator>();
    Square_cell_Operator End;
    Square_cell_Operator Start;
    List<Square_cell_Operator> Path;

    Color colPath = new Color(15, 100, 0, 255);// ## Заплатка
    Color colOS = new Color(255, 255, 255, 255);// ## Заплатка
    Color colCS = new Color(0, 0, 0, 255);// ## Заплатка

    public void Find_path(Square_cell_Operator start, Square_cell_Operator end)
    {

        // Инициализация Основных ТОЧЕК
        End = end;
        Start = start;
        
        //Если ЗС пуст (то скорее всего это старт системы поиска пути)
        if (Close_List.Count == 0) 
        {
            //и стартовым эелементом цикла будет Стартовая точка
            Open_List.Add(start);
            SquareCheckToOpenList(start);
            
        }
        else //Если ЗС не пуст то цикл поиска уже в процессе (пошли итерации)
        {
            // Проверим не пуст ли ОС (а на этот момент он должен быть уже чем то заполнен)
            if (Open_List.Count != 0) 
            {
                // Основной цикл вычисления окружающих кубов на дальнейшее рассмотрение
                SquareCheckToOpenList(SearchInOpen_listNextSquare());
            }
            print("Пути нет * ОС пуст");
        }
        
    } // НАЧАЛО ПОИСКА ПУТИ 
    
    void SquareCheckToOpenList(Square_cell_Operator CheckSquare)
    {
        Open_List.Remove(CheckSquare);
        Close_List.Add(CheckSquare);
        //Перебираем окружающие кубы Текущей точки
        foreach (Square_cell_Operator checkingSquare in CheckSquare.Around_Squares)
        {
            // Проверка нет ли барьера 
            if (!checkingSquare.barrier) 
            {
                // Проверка нет ли в ЗС
                if (!isCheckInClose_List(checkingSquare)) 
                {
                    //  Если куб в ОС
                    if (isCheckInOpen_List(checkingSquare)) 
                    {
                        // Проверка логичности продолжения пути по этому кубу  НА ТРУ
                        if (pathIsLogic(CheckSquare, checkingSquare))
                        {
                            // меняем у имеющегося куба в ОС его родителя на ТК также его значения FGH
                            AddSquareToOpenList(CheckSquare, checkingSquare); 
                        }

                    } //Куб не в ОС (штатная процедура добавления кубов в ОС)
                    else AddSquareToOpenList(CheckSquare, checkingSquare);
                }// Куб в ЗС игнорим
                else;


            }// Куб имеет не проходимый барьер игнорим
            else;   
            
        }
        
        
        if(Open_List.Count != 0)
        {
            if (isEndinOpen_List())
            {
                Path = Calculate_Path(End); // Цель найдена Пытаюсь проложить путь
                
                foreach(Square_cell_Operator square in Open_List)
                {
                    square.SetColor(colOS);
                }
                foreach (Square_cell_Operator square in Close_List)
                {
                    square.SetColor(colCS);
                }
                foreach (Square_cell_Operator squares in Path)
                {
                    squares.SetColor(colPath);
                }

            }
            else Find_path(Start, End); // ПОВТОР ЦИКЛА
        }
        

    }  // Проверка окружающих квадратиков нашего ПК








    // Не находится ли квадрат в ОС
    bool isCheckInOpen_List(Square_cell_Operator checkingSquare)
    {
        foreach(Square_cell_Operator CheckSq in Open_List)
        {
            if(CheckSq == checkingSquare)
            {
                return true;
            }     
        }
        return false;
    }  
    //Не находится ли квадрат в ЗС
    bool isCheckInClose_List(Square_cell_Operator checkingSquare)
    {
        foreach (Square_cell_Operator CheckSq in Close_List)
        {
            if (CheckSq == checkingSquare)
            {
                return true;
            }
        }
        return false;
    } 
    // Есть ли конечная цель в ОС
    bool isEndinOpen_List()
    {
        foreach (Square_cell_Operator check in Open_List)
        {
            if (check == End)
            {
                return true;
            }
        }
        return false;
    }  
    // Если на пути попался квадратик уже имеющийся в ОС проверяем логично ли на него наступить ************ НЕ МОГУ РАЗОБРАТЬСЯ В УСЛОВИИ
    bool pathIsLogic(Square_cell_Operator mySquare, Square_cell_Operator checkSquare)
    {
        if (checkSquare.A_Value[1] < mySquare.A_Value[1] + checkSquare.A_Value[1]) return false;
        else return true;
    }



    //Расчет пути при достижении цели по родителям 
    List<Square_cell_Operator> Calculate_Path(Square_cell_Operator end)
    {
        Square_cell_Operator step = end;
        List<Square_cell_Operator> CollectingPathNodes = new List<Square_cell_Operator>();
        CollectingPathNodes.Add(end);

        while(step.Parent)
        {
            CollectingPathNodes.Add(step.Parent);
            step = step.Parent;
            if(!step.Parent)
            {
                print("Путь готов");
                break;
            }
        }
        return CollectingPathNodes;
        
    }
    // Подбор нового шага (квадратика) для его проверки
    Square_cell_Operator SearchInOpen_listNextSquare()
    {
        int best_F = Open_List[0].A_Value[0];
        Square_cell_Operator bestChoice = Open_List[0];

        foreach (Square_cell_Operator checkingSquare in Open_List)
        {
            

            if(checkingSquare.A_Value[0] < best_F)
            {
                best_F = checkingSquare.A_Value[0];
                bestChoice = checkingSquare;
                
            }
           
        }
        return bestChoice;
    }
    // Добавляем квадратик в ОС даем ему родителя и значения FGH
    void AddSquareToOpenList(Square_cell_Operator parent, Square_cell_Operator check)  
    {
        check.Parent = parent;
        check.A_Value[2] = Calculate_H(check);
        check.A_Value[1] = Calculate_G(check, parent);
        check.A_Value[0] = check.A_Value[1] + check.A_Value[2];
        if(!isCheckInOpen_List(check)) 
        {
            Open_List.Add(check);
        }
    }

    // Вычесляем величину H для куба
    int Calculate_H(Square_cell_Operator check)
    {
        int lendth = Mathf.Abs(check.GetCoordinates()[0] - End.GetCoordinates()[0]) + Mathf.Abs(check.GetCoordinates()[1] - End.GetCoordinates()[1]);
        return  10 * lendth;
    }

    // Вычесляем величину G для куба
    int Calculate_G(Square_cell_Operator check, Square_cell_Operator parent)
    {
        int checkSum = check.GetCoordinates()[0] + check.GetCoordinates()[1];
        int parentSum = parent.GetCoordinates()[0] + parent.GetCoordinates()[1];
        
        if(Mathf.Abs(checkSum - parentSum) == 2 || Mathf.Abs(checkSum - parentSum) == 0)
        {
            return 14 + parent.A_Value[1];
        }
        return 10 + parent.A_Value[1];
    } 
    
    
}
