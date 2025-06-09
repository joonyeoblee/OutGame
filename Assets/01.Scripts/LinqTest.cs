using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class LinqTest : MonoBehaviour 
{
    private void Start()
    {
        // 학생 '리스트'
        List<Student> students = new List<Student>()
        {
            new Student() {Name = "허정범", Age = 28, Gender = "남"}, 
            new Student() {Name = "박수현", Age = 26, Gender = "여"}, 
            new Student() {Name = "박진혁", Age = 29, Gender = "남"}, 
            new Student() {Name = "이상진", Age = 28, Gender = "남"}, 
            new Student() {Name = "서민주", Age = 25, Gender = "여"}, 
            new Student() {Name = "전태준", Age = 27, Gender = "남"}, 
            new Student() {Name = "박순홍", Age = 27, Gender = "남"}, 
            new Student() {Name = "양성일", Age = 29, Gender = "남"}, 
        }; 
        
        // '컬렉션'에서 '데이터'를 '조회(나열)'하는 일이 많습니다.
        // C#은 이런 빈번한 작업을 편하게 하기 위해 LINQ문법
        // Language Intergrated Query
        // 쿼리 (Query) : 질의 (데이터를 요청하거나 검색하는 명령문)
        
        // "From, In, Select, Where, Order By" -> 데이터베이스 Select문과 비슷하다
        // 사실 이렇게는 잘 쓰이지 않는다.
        var all = from student in students select student;
        all = students.Where((student) => true);
        // 위 두 줄의 내용은 같다.
        
        foreach (var item in all)
        {
            Debug.Log(item);
        }
        
        Debug.Log("--------------------------");
        
        var girls = from student in students where student.Gender == "여" select student;
        girls = students.Where((student) => student.Gender == "여");
        foreach (var item in girls)
        {
            Debug.Log(item);
        }
        
        Debug.Log("--------------------------");
        
        var girls2 = from student in students 
            where student.Gender == "여"
            orderby student.Age
            select student;
        girls2 = students.Where((student) => student.Gender == "여").OrderBy(student => student.Age);
        List<Student> girllist = new List<Student>();
        // 정렬 알고리즘
        // foreach (var student in students)
        // {
        //     if (student.Gender == "여")
        //     {
        //         girllist.Add(student);
        //     }
        // }
        
        // 단점
        // IEnumerable은 내부적으로 커서를 만드는데 이것이 나중에 쓰레기가 된다.
        // ㄴ 메모리가 증가한다
        // ㄴ 쓰면 참 좋은데, 유니티 Update에서 사용은 비추

        Debug.Log("--------------------------");
        foreach (var item in girls2)
        {
            Debug.Log(item);
        }
        
        Debug.Log("--------------------------");
        
        int mansCount = students.Count(student => student.Gender == "남");
        Debug.Log($"남자 학생은 {mansCount}명 입니다");
        
        // Sum
        int totalAge = students.Sum(student => student.Age);
        Debug.Log($"학생의 총 나이는 {totalAge}살 입니다");
        
        // Averge
        
        // 조건 만족? ALL(모두가 만족하니?) vs Any(하나 이상이 만족하니?)
        // - 모두가 남자니?
        bool isAllMan = students.All(student => student.Gender == "남");
        
        // - 30살 아래가 한명이랃도 있니?
        bool is30 = students.Any(student => student.Age >= 30);
        
        // 정렬 문제
        // Sort 내장 함수는 내부적으로 마이크로 소프타가 이름 지은 인트로 소트를 쓴다.
        // 안트로 소트: 데이터의 크기, 종류등의 성질에 따라 Quick, Heap, Radix Sort를 짬뽕해서 적적히 쓰는 기법이다.
        students.Sort();
    }
}