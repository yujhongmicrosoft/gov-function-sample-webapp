import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FileUpload } from '../shared/file-upload';

@Injectable()
export class ModeratorApiService {

  constructor(private http: HttpClient) { }

  getAllImages() {
    return this.http.get<any[]>(`/api/images`);
  }

  getImage(id: string) {
    return this.http.get<any>(`/api/images/${id}`)
  }

  upload(uploadFile: FileUpload) {
    let formData = new FormData();
    formData.append('uploadFile', uploadFile.file);
    formData.append('description', uploadFile.description);
    return this.http.post<any>(`/api/images/upload`, formData);
  }

  deleteImage(id) {
    return this.http.delete(`/api/images/${id}`);
  }
}
