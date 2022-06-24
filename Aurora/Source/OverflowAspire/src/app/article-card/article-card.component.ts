import { Component, OnInit, Input } from '@angular/core';
import { Article } from 'Models/Article';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { data } from 'jquery';
import { isNull } from '@angular/compiler/src/output/output_ast';
import { AuthService } from '../auth.service';
import { application } from 'Models/Application';



@Component({
  selector: 'app-article-card',
  templateUrl: './article-card.component.html',
  styleUrls: ['./article-card.component.css']
})
export class ArticleCardComponent implements OnInit {
  @Input() artsrc: string = `${application.URL}/Article/GetArticlesByArticleStatusId(4)`;
  @Input() ShowStatus:boolean =true;
  totalLength: any;
  page: number = 1;
  searchTitle = "";
  searchAuthor = "";
  FromDate = new Date("0001-01-01");
  ToDate = new Date("0001-01-01");


  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    this.http
      .get<any>(this.artsrc,{headers:headers})
      .subscribe({next:(data) => {
        this.data = data;
        this.filteredData = data;
        this.totalLength = data.length;
      }});
  }
  public data: Article[] = [

  ];

  public filteredData: Article[] = [];
  samplefun(searchTitle: string, searchAuthor: string, FromDate: any, ToDate: any) {

    if (searchTitle.length == 0 && searchAuthor.length == 0 && FromDate == new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) this.data = this.filteredData


    //1.Search by title
    if (searchTitle.length != 0 && searchAuthor == '' && FromDate == new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      console.log("title")
      this.data = this.filteredData.filter(item => item.title.toLowerCase().includes(searchTitle.toLowerCase()));
    }
    //2.Search by Author
    else if (searchTitle == '' && searchAuthor.length != 0 && FromDate == new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      console.log("author")
      this.data = this.filteredData.filter(item => item.authorName.toLowerCase().includes(searchAuthor.toLowerCase()));
    }
    //3.Search by FromDate
    else if (searchTitle == '' && searchAuthor == '' && FromDate != new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      console.log("FromDate")
      this.data = this.filteredData.filter(item => new Date(item.date) >= new Date(FromDate));
    }
    //4.Search by ToDate
    else if (searchTitle == '' && searchAuthor == '' && FromDate == new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      console.log("ToDate")
      this.data = this.filteredData.filter(item => new Date(item.date) <= new Date(ToDate));
    }
    //5.search by title and author
    else if (searchTitle.length != 0 && searchAuthor.length != 0 && FromDate == new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      console.log("title&Author")
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && item.authorName.toLowerCase().includes(searchAuthor.toLowerCase()) });
    }
    //6.search by title and fromdate
    else if (searchTitle.length != 0 && searchAuthor == '' && FromDate != new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      console.log("title&FromDate")
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && new Date(item.date) >= new Date(FromDate) });
    }
    //7.search by title and Todate
    else if (searchTitle.length != 0 && searchAuthor == '' && FromDate == new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      console.log("title&ToDate")
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && new Date(item.date) <= new Date(ToDate) });
    }
    //8.search by author and fromdate
    else if (searchTitle == '' && searchAuthor.length != 0 && FromDate != new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      console.log("author&fromDate")
      this.data = this.filteredData.filter(item => { return item.authorName.toLowerCase().includes(searchAuthor.toLowerCase()) && new Date(item.date) >= new Date(FromDate) });
    }
    //9.search by author and todate
    else if (searchTitle == '' && searchAuthor.length != 0 && FromDate == new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      console.log("author&ToDate")
      this.data = this.filteredData.filter(item => { return item.authorName.toLowerCase().includes(searchAuthor.toLowerCase()) && new Date(item.date) <= new Date(ToDate) });
    }
    //10.search by fromdate and todate
    else if (searchTitle == '' && searchAuthor == '' && FromDate != new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      console.log("fromdate&TsearchTitle == ''oDate")
      this.data = this.filteredData.filter(item => { return new Date(item.date) >= new Date(FromDate) && new Date(item.date) <= new Date(ToDate) });
    }
    //11.search by author,title and fromdate
    else if (searchTitle.length != 0 && searchAuthor.length != 0 && FromDate != new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      console.log("title,author,fromdate")
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && item.authorName.toLowerCase().includes(searchAuthor.toLowerCase()) && new Date(item.date) >= new Date(FromDate) });
    }
    //12.search by author,title and Todate
    else if (searchTitle.length != 0 && searchAuthor.length != 0 && FromDate == new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      console.log("title,author,Todate")
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && item.authorName.toLowerCase().includes(searchAuthor.toLowerCase()) && new Date(item.date) <= new Date(ToDate) });
    }
     //13.search by author,Fromdate and Todate
     else if (searchTitle == '' && searchAuthor.length != 0 && FromDate != new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      console.log("author,fromdate,Todate")
      this.data = this.filteredData.filter(item => { return item.authorName.toLowerCase().includes(searchAuthor.toLowerCase()) && new Date(item.date) >= new Date(FromDate)&&new Date(item.date) <= new Date(ToDate) });
    }
     //14.search by Title,Fromdate and Todate
     else if (searchTitle.length != 0 && searchAuthor== ''&& FromDate != new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      console.log("title,fromdate,Todate")
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && new Date(item.date) >= new Date(FromDate)&&new Date(item.date) <= new Date(ToDate) });
    }
    //14.search by Title,Author,Fromdate and Todate
    else if (searchTitle.length != 0 && searchAuthor.length != 0 && FromDate != new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      console.log("title,author,fromdate,Todate")
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase())&&item.authorName.toLowerCase().includes(searchAuthor.toLowerCase()) && new Date(item.date) >= new Date(FromDate)&&new Date(item.date) <= new Date(ToDate) });
    }

  }
}
