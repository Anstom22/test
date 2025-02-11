using AutoMapper;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Office.Y2022.FeaturePropertyBag;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Sample1.CustomActionFilters;
using Sample1.Data;
using Sample1.Models.Domain;
using Sample1.Models.DTO;
using Sample1.Repositories;
using System.Data;
using System.Text.Json;

namespace Sample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(AppDbContext dbContext,IRegionRepository regionRepository,IMapper mapper,
            ILogger<RegionsController>logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(string? filterOn,string? filterQuery)
        {
        //    try
        //    {
        //        throw new Exception("custom exception");
                logger.LogInformation("Get All regions action method invoked");
                var regions = await regionRepository.GetAllAsync();
                //var regionDto = new List<RegionDTO>();
                //foreach (var region in regions)
                //{
                //    regionDto.Add(new RegionDTO()
                //    {
                //        Id = region.Id,
                //        Name = region.Name,
                //        Code = region.Code

                //    });

                //}
                logger.LogInformation($"Finished getting all regions request with data: {JsonSerializer.Serialize(regions)}");
                var regionDto = mapper.Map<List<RegionDTO>>(regions);
                return Ok(regionDto);

            //}
            //catch (Exception ex)
            //{
            //    logger.LogError(ex, ex.Message);
            //    throw;
            //}
            

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var region = await regionRepository.GetByIdAsync(id);
            //var rg = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null)
            {
            
                return NotFound();
            }
            //var regionDto = new RegionDTO
            //{
            //    Id = region.Id,
            //    Name = region.Name,
            //    Code = region.Code
            //};
            var regionDto= mapper.Map<RegionDTO>(region);
            return Ok(regionDto);

        }
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create(AddRegionDTO addRegionDTO)
        {
            //var region = new Region()
            //{
            //    Name = addRegionDTO.Name,
            //    Code = addRegionDTO.Code
            //};
            
                var region = mapper.Map<Region>(addRegionDTO);
                region = await regionRepository.CreateAsync(region);

                var regionDto = mapper.Map<RegionDTO>(region);

                //var regionDto = new RegionDTO
                //{
                //    Id = region.Id,
                //    Name = region.Name,
                //    Code = region.Code
                //};
                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
            
            

            
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRegion(int id, UpdateRegionDTO updateRegionDTO)
        {
            //var regions = new Region
            //{
            //    Code = updateRegionDTO.Code,
            //    Name = updateRegionDTO.Name
            //};
            var regions= mapper.Map<Region>(updateRegionDTO);
            regions = await regionRepository.UpdateRegionAsync(id, regions );
            if (regions == null)
            {
                return NotFound();
            }

            //var regionDto = new RegionDTO
            //{
            //    Id = regions.Id,
            //    Name = regions.Name,
            //    Code = regions.Code
            //};

            var regionDto = mapper.Map<RegionDTO>(regions);
            return Ok(regionDto);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegion(int id)
        {
            var region = await regionRepository.DeleteRegionAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            //var regionDto = new RegionDTO
            //{

            //    Id = region.Id, 
            //    Name = region.Name,
            //    Code = region.Code

            //};

            var regionDto = mapper.Map<RegionDTO>(region);
            return Ok(regionDto);
        }

        //[HttpGet("ExporttoExcel")]
        //public async Task<IActionResult> Export()
        //{
        //    var reg= await GetRegionDataAsync();
        //    using(XLWorkbook xb= new XLWorkbook())
        //    {
        //        var sheet1=xb.AddWorksheet(reg, "Regions Data");
        //        sheet1.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.Blue;
        //        sheet1.Column(1).Style.Font.FontColor = XLColor.Yellow;
        //        sheet1.Row(1).Style.Font.Bold = true;
        //        using(MemoryStream ms = new MemoryStream())
        //        {
        //            xb.SaveAs(ms);
        //            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Regions.xlsx");
        //        }

        //    }

        //}
        //[NonAction]
        //private async Task<DataTable> GetRegionDataAsync()
        //{
        //    DataTable dt = new DataTable();
        //    dt.TableName = "RegionData";
        //    dt.Columns.Add("Id",typeof(int));
        //    dt.Columns.Add("Code", typeof(string));
        //    dt.Columns.Add("Name",typeof(string));
            

        //    var region= await regionRepository.GetAllAsync();
           
        //    if(region.Count> 0)
        //    {
        //        foreach(var reg in region)
        //        {
        //            dt.Rows.Add(reg.Id,reg.Code,reg.Name);
        //        }
        //    }
        //    return dt;

        //}

        //[HttpGet("ExporttoExcel1")]
        //public async Task<IActionResult> ExportToExcel1()
        //{
        //    var region = await regionRepository.GetAllAsync();
        //    if(region== null || region.Count == 0)
        //    {
        //        return NotFound();
        //    }

        //    DataTable dt = new DataTable("Regions");
        //    dt.Columns.Add("Id", typeof(int));
        //    dt.Columns.Add("Code", typeof(string));
        //    dt.Columns.Add("Name", typeof(string));

        //    foreach(var reg in region)
        //    {
        //        dt.Rows.Add(reg.Id, reg.Name, reg.Code);
        //    }

        //    using(XLWorkbook wb= new XLWorkbook())
        //    {
        //        var sheet1 = wb.Worksheets.Add(dt,"RegionData");
        //        sheet1.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.Blue;
        //        sheet1.Row(1).Style.Font.FontColor = XLColor.Black;
        //        sheet1.Row(1).Style.Font.Bold = true;
        //        using(var ms=new MemoryStream())
        //        {
        //            wb.SaveAs(ms);
        //            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Regionssss.xlsx");
        //        }
        //    }


           

        //}

        [HttpGet("ExportToExcel")]
        public async Task<IActionResult> Export2()
        {
            var region = await GetRegionsForExport();
            if (region == null)
            {
                return NotFound();
            }

            using(XLWorkbook xb=new XLWorkbook())
            {
                var sheet1 = xb.AddWorksheet(region, "RegionData");
                sheet1.Row(1).CellsUsed().Style.Fill.BackgroundColor=XLColor.AshGrey;

                using(MemoryStream ms= new MemoryStream())
                {
                    xb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Region2.xlsx");
                }
            }
        }


        [NonAction]
        private async Task<DataTable> GetRegionsForExport()
        {
            var region = await regionRepository.GetAllAsync();
            if(region==null|| region.Count == 0)
            {
                return null;
            }
            DataTable dt = new DataTable("Regions");
            dt.Columns.Add("Id",typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Code", typeof(string));

            foreach(var reg in region)
            {
                dt.Rows.Add(reg.Id,reg.Name,reg.Code);
            }
            return dt;
        }


        [HttpGet("export")]
        public async Task<IActionResult> ExportToExcel()
        {
            var regions = await regionRepository.GetAllAsync();

            if (regions == null || regions.Count == 0)
                return NotFound("No regions available.");

            // Create an Excel workbook
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Regions List");

                // Add headers with formatting
                worksheet.Cell(1, 1).Value = "Region ID";
                worksheet.Cell(1, 2).Value = "Region Name";
                worksheet.Cell(1, 3).Value = "Region Code";

                worksheet.Range("A1:C1").Style.Font.Bold = true;
                worksheet.Range("A1:C1").Style.Fill.BackgroundColor = XLColor.LightGray;
                worksheet.Range("A1:C1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Insert data
                int row = 2;
                foreach (var reg in regions)
                {
                    worksheet.Cell(row, 1).Value = reg.Id;
                    worksheet.Cell(row, 2).Value = reg.Name;
                    worksheet.Cell(row, 3).Value = reg.Code;
                    
                    row++;
                }

                // Auto-adjust columns for better readability
                worksheet.Columns().AdjustToContents();

                // Return file
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RegionList.xlsx");
                }
            }
        }
    }
}
