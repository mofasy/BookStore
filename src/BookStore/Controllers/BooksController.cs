using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.API.DTOs.Book;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : MainController
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(IMapper mapper, IBookService bookService)
        {
            _mapper = mapper;
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAll();

            if (books == null) return NotFound();

            return Ok(_mapper.Map<IEnumerable<BookResultDTO>>(books));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetById(id);

            if (book == null) return NotFound();

            return Ok(_mapper.Map<BookResultDTO>(book));
        }

        [HttpGet]
        [Route("get-books-by-category/{categoryId:int}")]
        public async Task<IActionResult> GetBooksByCategory(int categoryId)
        {
            var books = await _bookService.GetBooksByCategory(categoryId);

            if (!books.Any()) return NotFound();

            return Ok(_mapper.Map<IEnumerable<BookResultDTO>>(books));
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookAddDTO bookDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            var book = _mapper.Map<Book>(bookDTO);

            var result = await _bookService.Add(book);

            if (result == null) return BadRequest();

            return Ok(_mapper.Map<BookResultDTO>(result));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, BookEditDTO bookDTO)
        {
            if (id != bookDTO.Id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var book = _mapper.Map<Book>(bookDTO);

            await _bookService.Update(book);

            return Ok(bookDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            var book = await _bookService.GetById(id);

            if (book == null) return NotFound();

            var result = await _bookService.Remove(book);

            if (!result) return BadRequest();

            return Ok();
        }

        [Route("search/{bookname}")]
        [HttpGet]
        public async Task<ActionResult<List<Book>>> Search(string bookname)
        {
            var books = _mapper.Map<List<Book>>(await _bookService.Search(bookname));

            if (books == null || books.Count == 0) return NotFound("No book was found!!"); ;

            return Ok(books);
        }

        [Route("search-book-by-category/{searchedValue}")]
        [HttpGet]
        public async Task<ActionResult<List<Book>>> SearchBookByCategory(string searchedValue)
        {
            var books = _mapper.Map<List<Book>>(await _bookService.SearchBookByCategory(searchedValue));

            if (!books.Any()) return NotFound("No book was found!!");

            return Ok(_mapper.Map<IEnumerable<BookResultDTO>>(books));
        }
    }
}
