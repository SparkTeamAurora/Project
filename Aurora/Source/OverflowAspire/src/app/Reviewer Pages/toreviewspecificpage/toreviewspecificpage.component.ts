import { Component, OnInit } from '@angular/core';
import { Article } from 'Models/Article';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { ConnectionService } from 'src/app/Services/connection.service';
import { Toaster } from 'ngx-toast-notifications';
import { User } from 'Models/User';

@Component({
  selector: 'app-toreviewspecificpage',
  templateUrl: './toreviewspecificpage.component.html',
  styleUrls: ['./toreviewspecificpage.component.css']
})
export class ToreviewspecificpageComponent implements OnInit {
  articleId: number = 0;
  error = ""
  userid: any;
  public data: Article = new Article();

  constructor(private route: ActivatedRoute, private routing: Router, private connection: ConnectionService, private toaster: Toaster) { }

  // Get article by article id.
  ngOnInit(): void {
    if (AuthService.GetData("token") == null) {
      this.toaster.open({ text: 'Your Session has been Expired', position: 'top-center', type: 'warning' })
      this.routing.navigateByUrl("")
    }
    if (!AuthService.IsReviewer()) {
      this.routing.navigateByUrl("")
    }
    this.route.params.subscribe(params => {
      this.articleId = params['articleId'];
      this.connection.GetArticle(this.articleId)
        .subscribe({
          next: (data: Article) => {
            this.data = data;
          },
          error: (error: any) => this.error = error.error.message,
        });
      this.connection.GetCurrentApplicationUser()
        .subscribe({
          next: (data: User) => {
            this.userid = data.userId;
          },
          error: (error: any) => this.error = error.error.message,
        });
    });
  }
  isShown: boolean = false ; // hidden by default
  toggleShow() {
  
  this.isShown = ! this.isShown;
  
  }
  isShow: boolean = false ; // hidden by default
  toggleShown() {
  
  this.isShow = ! this.isShow;
  
  }

  PublishArticle(articleId: number) {
    this.connection.ApproveArticle(articleId)
      .subscribe({
        next: (data: any) => {
        },
        error: (error: any) => this.error = error.error.message,
      });
    this.toaster.open({ text: 'Article Published successfully', position: 'top-center', type: 'success' })
    this.routing.navigateByUrl("/ToReview");
  }

  ChangeToUnderReview(articleId: number) {
    this.connection.ChangeToUnderReview(articleId)
      .subscribe({
        next: (data: any) => {
        },
        error: (error: any) => this.error = error.error.message,
        complete: () => {
          this.ngOnInit()
          this.toaster.open({ text: 'Checked in successfully', position: 'top-center', type: 'warning' })
        }
      });
    setTimeout(
      () => {
        location.reload();
      },
      1000
    );
  }
}