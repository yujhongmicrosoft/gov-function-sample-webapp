import { Component, OnInit } from '@angular/core';
import { ModeratorApiService } from '../services/moderator-api.service';
import * as _ from 'lodash';

@Component({
  selector: 'app-images',
  templateUrl: './images.component.html',
  styleUrls: ['./images.component.css']
})
export class ImagesComponent implements OnInit {
  public allImages = [];
  public images = [];
  public loading = false;
  public approvedTitle = 'Approved';
  public rejectedTitle = 'Rejected';
  public pendingTitle = 'Pending';
  private currentStatus = 'Approved';

  constructor(private modApi: ModeratorApiService) { }

  ngOnInit() {
    this.loading = true;
    this.modApi.getAllImages().subscribe(data => {
      console.log('**get all images data', data);
      this.allImages = data;
      this.setTitles();
      //this.filter('Approved');
      this.loading = false;
    });
  }

  deleteImage(id) {
    this.loading = true;
    this.modApi.deleteImage(id).subscribe(() => {
      _.remove(this.allImages, { id: id });
      this.setTitles();
      this.loading = false;
    });
  }

  filter(status) {
    this.images = _.filter(this.allImages, { status: status});
  }

  setTitles() {
    let groups = _.groupBy(this.allImages, 'status');
    this.approvedTitle = `Approved (${groups.Approved ? groups.Approved.length : 0})`;
    this.rejectedTitle = `Rejected (${groups.Rejected ? groups.Rejected.length : 0})`;
    this.pendingTitle = `Pending (${groups.Pending ? groups.Pending.length : 0})`;
    this.filter(this.currentStatus);
  }

  tabChanged($event) {
    this.currentStatus = $event.nextId;
    this.images = _.filter(this.allImages, { status: this.currentStatus });
  }

}
