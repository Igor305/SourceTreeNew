import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { environment } from '../../environments/environment';
import { PostCreateRequestAuthorModel } from '../models/request/postCreate.request.author.model';
import { ResponseAuthorModel } from '../models/response/response.author.model';



@Injectable({
  providedIn: 'root'
})
export class AuthorService {

  constructor(private http: HttpClient) { }

  public async getAll() : Promise<ResponseAuthorModel> {
    const authors  = await this.http.get<ResponseAuthorModel>(
      environment.protocol + environment.host + environment.port +
      environment.author + environment.getall).toPromise();

    return authors;
  }

  public async getAllWithoutRemove() : Promise<ResponseAuthorModel> {
    const authors  = await this.http.get<ResponseAuthorModel>(
      environment.protocol + environment.host + environment.port +
      environment.author + environment.getallwithoutremove).toPromise();

    return authors;
  }

  public async getPagination() : Promise<ResponseAuthorModel> {
    const authors  = await this.http.get<ResponseAuthorModel>(
      environment.protocol + environment.host + environment.port +
      environment.author + environment.pagination).toPromise();

    return authors;
  }

  public async getById(authorId: string) : Promise<ResponseAuthorModel> {
    const authors  = await this.http.get<ResponseAuthorModel>(
      environment.protocol + environment.host + environment.port +
      environment.author + authorId).toPromise();

    return authors;
  }

  public async getByFullName() : Promise<ResponseAuthorModel> {
    const authors  = await this.http.get<ResponseAuthorModel>(
      environment.protocol + environment.host + environment.port +
      environment.author + environment.getByFullName).toPromise();

    return authors;
  }

  public async getFiltration() : Promise<ResponseAuthorModel> {
    const authors  = await this.http.get<ResponseAuthorModel>(
      environment.protocol + environment.host + environment.port +
      environment.author + environment.filtration).toPromise();

    return authors;
  }

  public async postCreate(postCreateRequestAuthorModel : PostCreateRequestAuthorModel) : Promise<ResponseAuthorModel> {
    const authors  = await this.http.post<ResponseAuthorModel>(
      environment.protocol + environment.host + environment.port +
      environment.author + environment.create, postCreateRequestAuthorModel).toPromise();

    return authors;
  }

  public async putUpdate(postCreateRequestAuthorModel : PostCreateRequestAuthorModel, authorId: string ) : Promise<ResponseAuthorModel> {
    const authors  = await this.http.post<ResponseAuthorModel>(
      environment.protocol + environment.host + environment.port +
      environment.author + authorId, postCreateRequestAuthorModel).toPromise();

    return authors;
  }

  public async delete(authorId: string ) : Promise<ResponseAuthorModel> {
    const authors  = await this.http.delete<ResponseAuthorModel>(
      environment.protocol + environment.host + environment.port +
      environment.author + authorId).toPromise();

    return authors;
  }
}