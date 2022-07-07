import { Component, OnInit } from '@angular/core';
import { Article } from 'Models/Article';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { ConnectionService } from 'src/app/Services/connection.service';
import { Toaster } from 'ngx-toast-notifications';

@Component({
  selector: 'app-toreviewspecificpage',
  templateUrl: './toreviewspecificpage.component.html',
  styleUrls: ['./toreviewspecificpage.component.css']
})
export class ToreviewspecificpageComponent implements OnInit {
  articleId: number = 0;

  constructor(private route: ActivatedRoute, private routing: Router, private connection: ConnectionService,private toaster: Toaster) { }
  // Get article by article id.
  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.routing.navigateByUrl("")
    if (!AuthService.GetData("Reviewer")) {
      this.routing.navigateByUrl("")
    }
    this.route.params.subscribe(params => {
      this.articleId = params['articleId'];
      this.connection.GetArticle(this.articleId)
        .subscribe({
          next: (data: Article) => {
            this.data = data;
          }
        });
    });
  }
  public data: Article = new Article();

  PublishArticle(articleId: number) {
    this.connection.ApproveArticle(articleId)
      .subscribe({
        next: (data: any) => {
        }
      });
    this.toaster.open({ text: 'Article Published successfully', position: 'top-center', type: 'success' })
    this.routing.navigateByUrl("/ToReview");

  }

  RejectArticle(articleId: number) {
    this.connection.RejectArticle(articleId)
      .subscribe({
        next: (data: any) => {
        }
      });
    this.toaster.open({ text: 'Article Rejected successfully', position: 'top-center', type: 'warning' })
    this.routing.navigateByUrl("/ToReview");
  }

  ChangeToUnderReview(articleId: number) {
    this.connection.ChangeToUnderReview(articleId)
      .subscribe({
        next: (data: any) => {
          
        }
      });
      this.ngOnInit()
      this.toaster.open({ text: 'Checked in successfully', position: 'top-center', type: 'warning' }) 
  }
}