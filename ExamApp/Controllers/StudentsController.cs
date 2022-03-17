using System;
using System.Linq;
using ExamApp.Context;
using ExamApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Controllers;

[ApiController]
[Route("students")]
public class StudentsController : ControllerBase
{
    private IStudentsService _service;

    public StudentsController(IStudentsService service)
    {
        this._service = service;
    }

    // Method never gets invoked.
    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            return Ok(_service.GetAllStudents());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // Method never gets invoked.
    [HttpGet]
    public IActionResult Get(int id)
    {
        try
        {
            return Ok(_service.GetAllStudents().First(x => x.Id == id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // Method never gets invoked.
    [HttpPost]
    public IActionResult Create(Student student)
    {
        try
        {
            return Ok(_service.AddStudent(student));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // Method never gets invoked.
    [HttpPost]
    public IActionResult Update(int id, Student student)
    {
        try
        {
            _service.Modify(id, student);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}