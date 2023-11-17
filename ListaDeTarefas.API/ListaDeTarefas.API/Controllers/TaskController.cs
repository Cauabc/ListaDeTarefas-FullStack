using ListaDeTarefas.API.Interfaces;
using ListaDeTarefas.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ListaDeTarefas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]

        public async Task<IEnumerable<TaskModel>> Get()
        {
            return await _taskRepository.GetAll();
        }

        [HttpGet("gerarExcel")]
        public async Task<IActionResult> GerarExcel()
        {
            var tasks = await _taskRepository.GetAll();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Tarefas");

                worksheet.Cells.LoadFromCollection(tasks, true);
                var headerRange = worksheet.Cells[worksheet.Dimension.Start.Row, worksheet.Dimension.Start.Column, worksheet.Dimension.Start.Row, worksheet.Dimension.End.Column];
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkGreen);

                var tableRange = worksheet.Cells[worksheet.Dimension.Address];

                tableRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                tableRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                tableRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                tableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    
                worksheet.Column(1).Width = 40;
                worksheet.Column(2).AutoFit();
                worksheet.Column(3).Width = 25;

                foreach (var cell in headerRange)
                {
                    cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    cell.Style.Font.Color.SetColor(System.Drawing.Color.White);
                }

                var ultimaColuna = worksheet.Dimension.End.Column;
                var valoresUltimaColuna = worksheet.Cells[worksheet.Dimension.Start.Row + 1, ultimaColuna, worksheet.Dimension.End.Row, ultimaColuna];
                valoresUltimaColuna.Style.Font.Bold = true;

                var fileBytes = package.GetAsByteArray();

                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "tarefas.xlsx");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            if (await _taskRepository.GetById(id) == null)
            {
                return NotFound("Tarefa não encontrada!");
            }
            return Ok(await _taskRepository.GetById(id));
        }

        [HttpPost]

        public async Task<IActionResult> Create(TaskModel request)
        {
            try
            {
                _taskRepository.Add(request);
                if (await _taskRepository.SaveAllAsync())
                {
                    return Ok(request);
                }
                return BadRequest("Erro ao criar tarefa");
            }
            catch(Exception ex)
            {
                return BadRequest("Erro ao criar tarefa: "+ ex.Message);
            }
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                _taskRepository.Delete(id);
                if (await _taskRepository.SaveAllAsync())
                {
                    return Ok();
                }
                return BadRequest("Erro ao salvar tarefa");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id,[FromBody] TaskModel request)
        {
            try
            {
                _taskRepository.Update(id, request);
                if (await _taskRepository.SaveAllAsync())
                {
                    return Ok();
                }
                return BadRequest("Erro ao salvar tarefa");
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
