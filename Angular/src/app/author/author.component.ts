import { Component, OnInit } from '@angular/core';
import { AuthorService } from '../services/author.service';
import { ResponseAuthorModel } from '../models/response/response.author.model';
import { PostCreateRequestAuthorModel } from '../models/request/postCreate.request.author.model';

@Component({
  selector: 'app-author',
  templateUrl: './author.component.html',
  styleUrls: ['./author.component.scss']
})
export class AuthorComponent implements OnInit {
  postCreateRequestAuthorModel : PostCreateRequestAuthorModel = {};
  responseAuthorModel : ResponseAuthorModel = {};
  authorId : string = '';

  constructor(private authorService: AuthorService) { }

  public async getAll(){
    this.responseAuthorModel = await this.authorService.getAll();
  }

  public async getAllWithoutRemove(){
    this.responseAuthorModel = await this.authorService.getAllWithoutRemove();
  }

  public async getByFullName(){
    this.responseAuthorModel = await this.authorService.getByFullName();
  }

  public async getById(){
    this.responseAuthorModel = await this.authorService.getById(this.authorId);
  }

  public async getPagination(){
    this.responseAuthorModel = await this.authorService.getPagination();
  }

  public async getFiltration(){
    this.responseAuthorModel = await this.authorService.getFiltration();
  }

  public async postCreate(){
    this.responseAuthorModel = await this.authorService.postCreate(this.postCreateRequestAuthorModel);
  }

  public async putUpdate(){
    this.responseAuthorModel = await this.authorService.putUpdate(this.postCreateRequestAuthorModel, this.authorId);
  }

  public async delete(){
    this.responseAuthorModel = await this.authorService.delete(this.authorId);
  }
  
  async ngOnInit() {
  }

}
