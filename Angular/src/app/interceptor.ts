import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { AuthService } from './services/auth.service';
import { PostResponseRefreshTokenModel } from './models/response/post.response.refreshToken.model';
import { PostRequestRefreshTokenModel } from './models/request/post.request.refreshToken.model';

@Injectable()
export class Interceptor implements HttpInterceptor {

    constructor(private authService: AuthService) {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const access = localStorage.getItem('accessToken');           
        if (access !== null){
            const accesstoken = 'Bearer ' + localStorage.getItem('accessToken');
            const refreshtoken = localStorage.getItem('refreshToken');
            this.authService.userData(accesstoken);
            const isExpired = this.authService.isExpared(accesstoken);
            localStorage.setItem('isExpired', isExpired.toString());
            const auth = req.clone({
                setHeaders: {
                    'Authorization': accesstoken,
                    'Content-Type': 'application/json'
                }
            });
            return next.handle(auth).pipe(
                catchError(
                    async error  => {
                        let status = false;
                        if (error.status === 401) {
                            const isExpired = this.authService.isExpared(refreshtoken);
                            if (isExpired){
                                const postRequestRefreshTokenModel: PostRequestRefreshTokenModel = {};                        
                                postRequestRefreshTokenModel.tokenString = refreshtoken;
                                let postResponseRefreshTokenModel: PostResponseRefreshTokenModel = await this.authService.refreshToken(postRequestRefreshTokenModel);
                                status = postResponseRefreshTokenModel.status;             
                                const accessToken = postResponseRefreshTokenModel.accessToken;                  
                                const refreshToken = postResponseRefreshTokenModel.refreshToken;
                                localStorage.setItem('accessToken', accessToken);
                                localStorage.setItem('refreshToken', refreshToken);
                            }
                            if (!isExpired){
                                window.location.href = '/login';
                            }
                        }
                        throw { status }
                    }
                )
            )
        }
        if (access === null){
            const auth = req.clone({
                setHeaders: {
                    'Content-Type': 'application/json'
                }
            });
            return next.handle(auth);
        }
    }
}