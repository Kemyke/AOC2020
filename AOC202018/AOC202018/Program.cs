using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC202018
{
    class Program
    {
        //class Expression
        //{
        //    public Expression Lp { get; set; }
        //    public Expression Rp { get; set; }
        //    public string Op { get; set; }

        //    public long Eval()
        //    {
        //        switch(Op)
        //        {
        //            case "+": return Lp.Eval() + Rp.Eval();
        //            case "*": return Lp.Eval() * Rp.Eval();
        //            default:
        //                return long.Parse(Op);
        //        }
        //    }

        //    public static Expression Parse(string expr)
        //    {
        //        if(!expr.Contains("+") && !expr.Contains("*"))
        //        {
        //            return new Expression { Op = expr };
        //        }


        //    }
        //}

        static long Eval(string expr)
        {
            if (!expr.Contains("+") && !expr.Contains("*"))
            {
                return long.Parse(expr);
            }

            long value = 0;
            for (int i = 0; i < expr.Length; i += 2)
            {
                long val;
                char op;
                if (i == 0)
                {
                    op = ' ';
                }
                else
                {
                    op = expr[i - 1];
                }
                if (expr[i] == '(')
                {
                    int endIdx = 0;
                    int level = 0;
                    for(int j = i + 1; j < expr.Length;j++)
                    {
                        if(expr[j]==')')
                        {
                            level--;
                            if(level<0)
                            {
                                endIdx = j;
                                break;
                            }
                        }
                        else if (expr[j]=='(')
                        {
                            level++;
                        }
                    }
                    var se = expr.Substring(i + 1, endIdx - i - 1);
                    val = Eval(se);
                    i = endIdx;
                }
                else
                {
                    val = long.Parse(expr[i].ToString());
                }

                switch (op)
                {
                    case '+': value += val; break;
                    case '*': value *= val; break;
                    case ' ': value = val; break;
                    default:
                        throw new Exception();
                }
            }
            return value;
        }

        static string AddParenthesis(string expr)
        {
            string ret = expr;
            List<Tuple<int,int>> expressions = new List<Tuple<int, int>>();
            List<char> operators = new List<char>();
            for (int i = 0; i < expr.Length; i += 2)
            {
                if (i > 0)
                {
                    operators.Add(expr[i - 1]);
                }

                if (expr[i] == '(')
                {
                    int endIdx = 0;
                    int level = 0;
                    for (int j = i + 1; j < expr.Length; j++)
                    {
                        if (expr[j] == ')')
                        {
                            level--;
                            if (level < 0)
                            {
                                endIdx = j;
                                break;
                            }
                        }
                        else if (expr[j] == '(')
                        {
                            level++;
                        }
                    }                    
                    expressions.Add(new Tuple<int, int>(i, endIdx - i));
                    i = endIdx;
                }
                else
                {
                    expressions.Add(new Tuple<int, int>(i, 1));
                }

            }

            int sc = 0;
            for(int i = 0; i < operators.Count; i++)
            {
                if(operators[i] == '+')
                {
                    ret = ret.Insert(sc++ + expressions[i].Item1, "(");
                    ret = ret.Insert(sc++ + expressions[i + 1].Item1 + expressions[i + 1].Item2, ")");
                }
            }

            foreach(var e in expressions.Where(ee=>ee.Item2 > 1))
            {
                AddParenthesis(expr.Substring(e.Item1 + 1, e.Item2 - 1));
            }

            return ret;
        }

        static void Main(string[] args)
        {
            List<string> exprs = File.ReadAllLines("input18.txt").ToList();
            long ret1 = 0;
            foreach (var expr in exprs)
            { 
                var e = expr.Replace(" ", "").Replace(Environment.NewLine, "");
                ret1 += Eval(e);
            }

            long ret2 = 0;
            foreach (var expr in exprs)
            {
                var e = expr.Replace(" ", "").Replace(Environment.NewLine, "");
                e = AddParenthesis(e);
                ret2 += Eval(e);
            }
            Console.WriteLine("Hello World!");
        }
    }
}
